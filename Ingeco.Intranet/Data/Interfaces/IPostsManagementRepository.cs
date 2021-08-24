using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Interfaces
{
    public interface IPostsManagementRepository
    {
        Task CreateCategory(Category newCategory);
        Task DeleteCategory(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(Guid id);
        Task UpdateCategory(Category category);
        Task<bool> ExistCategory(Guid id);
        Task<int> GetCategoryPostCount(Guid categoryId);
    }
}