using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Services.Abstructs
{
    public interface ICategoryServices
    {
        public Task<List<Category>> GetCategoryList();
        Task<CategoryDto?> GetCategoryById(int id);
        Task<CategoryDto> CreateCategory(CreateCategoryDto dto);
        Task<bool> UpdateCategory(int id, UpdateCategoryDto dto);
        Task<bool> DeleteCategory(int id);
    }
}
