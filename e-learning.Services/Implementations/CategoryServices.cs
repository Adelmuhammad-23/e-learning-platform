using AutoMapper;
using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class CategoryServices : ICategoryServices
    {
        #region Fields
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        #endregion

        #region Constructors
        public CategoryServices(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        #endregion

        #region Functions
        public async Task<List<Category>> GetCategoryList()
        {
            var categories = await _categoryRepository.GetAllCategory();
            return categories;
        }
        public async Task<CategoryDto?> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            var created = await _categoryRepository.CreateCategory(category);
            return _mapper.Map<CategoryDto>(created);
        }

        public async Task<bool> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            var categoryMapping = _mapper.Map(dto, category);
            return await _categoryRepository.UpdateCategory(categoryMapping);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await _categoryRepository.DeleteCategory(id);
        }
    }
    #endregion
}
