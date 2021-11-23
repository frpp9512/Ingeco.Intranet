using Ingeco.Intranet.Data.Contexts;
using Ingeco.Intranet.Data.Interfaces;
using Ingeco.Intranet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Repositories
{
    public class PostsManagementRepository : IPostsManagementRepository
    {
        private readonly WebDataContext _dataContext;

        public PostsManagementRepository(WebDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Categories management

        public async Task<Category> GetCategoryAsync(Guid id)
            => await _dataContext.Categories.FindAsync(id);

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
            => await _dataContext.Categories.ToListAsync();

        public async Task CreateCategory(Category newCategory)
        {
            if (newCategory is null)
            {
                throw new ArgumentNullException("The new category cannot be null.");
            }
            await _dataContext.Categories.AddAsync(newCategory);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException("The edited category cannot be null.");
            }
            _dataContext.Update(category);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException("The category to be removed cannot be null.");
            }
            _dataContext.Remove(category);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<int> GetCategoryPostCount(Guid categoryId)
            => await _dataContext.Posts.CountAsync(p => p.CategoryId == categoryId);

        public async Task<bool> ExistCategory(Guid id)
            => await _dataContext.Categories.AnyAsync(c => c.Id == id);

        #endregion

        #region Posts management

        public async Task CreatePostAsync(Post newPost)
        {
            await _dataContext.AddAsync(newPost);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CreateWebMediaAsync(WebMedia newMedia)
        {
            await _dataContext.AddAsync(newMedia);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CreateWebMediaAsync(IEnumerable<WebMedia> newMedia)
        {
            await _dataContext.AddRangeAsync(newMedia);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(Guid id, bool loadCategory = false, bool loadMedia = false, bool loadUser = false)
        {
            var post = _dataContext.Posts.Where(p => p.Id == id);
            if (loadCategory)
            {
                post = post.Include(p => p.Category);
            }
            if (loadMedia)
            {
                post = post.Include(p => p.Media);
            }
            if (loadUser)
            {
                post = post.Include(p => p.PostedBy);
            }
            return await post.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsForPostAsync(Guid postId, int depth)
        {
            var comments = await _dataContext.Comments.Where(c => c.PostId == postId && !c.IsReply)
                                                      .OrderByDescending(c => c.Created)
                                                      .Include(c => c.User)
                                                      .ToListAsync();
            if (depth > 0)
            {
                foreach (var comment in comments)
                {
                    await LoadRepliesUntilDepthAsync(comment, depth);
                }
            }
            return comments;
        }

        private async Task LoadRepliesUntilDepthAsync(Comment comment, int depth)
        {
            if (depth <= 0)
            {
                return;
            }
            await LoadRepliesForCommentAsync(comment);
            foreach (var reply in comment.Replies)
            {
                await LoadRepliesUntilDepthAsync(reply, depth - 1);
            }
        }

        public async Task LoadRepliesForCommentAsync(Comment comment)
            => comment.Replies = await GetRepliesForCommentAsync(comment.Id);


        public async Task<IEnumerable<Comment>> GetRepliesForCommentAsync(Guid commentId)
        {
            var replies = await _dataContext.Comments.Where(c => c.RepliedToId == commentId)
                                                     .OrderByDescending(c => c.Created)
                                                     .Include(c => c.User)
                                                     .ToListAsync();
            return replies;
        }

        public async Task<int> GetTotalRepliesForCommentAsync(Guid commentId)
        {
            var replies = await _dataContext.Comments.Where(c => c.RepliedToId == commentId).Select(c => c.Id).ToListAsync();
            var count = replies.Count;
            count += replies.Sum(c => GetTotalRepliesForCommentAsync(c).GetAwaiter().GetResult());
            return count;
        }

        public async Task<int> GetTotalCommentsCountForPostAsync(Guid postId)
            => await _dataContext.Comments.Where(c => c.PostId == postId).CountAsync();

        public async Task<WebMedia> GetMediaAsync(Guid id)
        {
            var media = await _dataContext.Media.FindAsync(id);
            return media;
        }

        public async Task<Comment> GetCommentAsync(Guid id)
        {
            var comment = await _dataContext.Comments.FindAsync(id);
            return comment;
        }

        public async Task<bool> ExistPostAsync(Guid id)
            => await _dataContext.Posts.AnyAsync(p => p.Id == id);

        public async Task<bool> ExistCommentInPostAsync(Guid postId, Guid commentId)
            => await _dataContext.Comments.AnyAsync(c => c.Id == commentId && c.PostId == postId);

        public async Task CreateCommentAsync(Comment comment)
        {
            await _dataContext.Comments.AddAsync(comment);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsForPageAsync(int page, int postsPerPage)
        {
            if (postsPerPage <= 0 || page <= 0)
            {
                throw new ArgumentException("The arguments must be valid.");
            }
            int startingIndex = (page - 1) * postsPerPage;
            if (startingIndex >= await _dataContext.Posts.CountAsync())
            {
                startingIndex = 0;
            }
            var posts = await _dataContext.Posts.OrderByDescending(p => p.Created)
                                                .Skip(startingIndex)
                                                .Take(postsPerPage)
                                                .Include(p => p.Category)
                                                .Include(p => p.Media)
                                                .ToListAsync();

            return posts;
        }

        public async Task<int> GetTotalPostsPagesAsync(int postsPerPage)
            => (int)Math.Ceiling((decimal)(await _dataContext.Posts.CountAsync() / postsPerPage));

        public async Task<IEnumerable<Post>> GetLatestPublicPostsAsync(int amount)
        {
            var posts = await _dataContext.Posts.Where(p => p.Public)
                                                .OrderByDescending(p => p.Created)
                                                .Take(amount)
                                                .Include(p => p.Category)
                                                .Include(p => p.Media)
                                                .ToListAsync();
            return posts;
        }

        #endregion
    }
}