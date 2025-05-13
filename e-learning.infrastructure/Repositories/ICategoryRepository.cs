using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategory();
        Task<Category?> GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);

    }
}
