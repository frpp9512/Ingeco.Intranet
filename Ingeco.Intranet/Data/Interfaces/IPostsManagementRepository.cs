using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Interfaces
{
    public interface IPostsManagementRepository
    {
        #region Categories management

        Task CreateCategory(Category newCategory);
        Task DeleteCategory(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(Guid id);
        Task UpdateCategory(Category category);
        Task<bool> ExistCategory(Guid id);
        Task<int> GetCategoryPostCount(Guid categoryId);

        #endregion

        #region Posts management

        Task CreatePostAsync(Post newPost);

        Task CreateWebMediaAsync(WebMedia newMedia);

        Task CreateWebMediaAsync(IEnumerable<WebMedia> newMedia);

        Task<Post> GetPostAsync(Guid id, bool loadCategory = false, bool loadMedia = false, bool loadUser = false);

        Task<IEnumerable<Comment>> GetCommentsForPostAsync(Guid postId, int depth);

        Task<WebMedia> GetMediaAsync(Guid id);

        Task LoadRepliesForCommentAsync(Comment comment);

        Task<IEnumerable<Comment>> GetRepliesForCommentAsync(Guid commentId);

        Task<int> GetTotalCommentsCountForPostAsync(Guid postId);

        Task<int> GetTotalRepliesForCommentAsync(Guid commentId);

        Task<Comment> GetCommentAsync(Guid id);

        Task<bool> ExistPostAsync(Guid id);

        Task<bool> ExistCommentInPostAsync(Guid postId, Guid commentId);

        Task CreateCommentAsync(Comment comment);

        Task<IEnumerable<Post>> GetPostsForPageAsync(int page, int postsPerPage);

        Task<IEnumerable<Post>> GetLatestPublicPostsAsync(int amount);

        Task<int> GetTotalPostsPagesAsync(int postsPerPage);

        #endregion
    }
}