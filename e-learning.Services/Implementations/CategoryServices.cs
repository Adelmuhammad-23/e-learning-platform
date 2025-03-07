using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class CategoryServices : ICategoryServices
    {
        #region Fields
        private readonly ICategoryRepository _categoryRepository;


        #endregion

        #region Constructors
        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        #endregion

        #region Functions
        public async Task<List<Category>> GetCategoryList()
        {
            var categories = await _categoryRepository.GetAllCategory();
            return categories;
        }

        #endregion
    }
}
