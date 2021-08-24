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
    }
}