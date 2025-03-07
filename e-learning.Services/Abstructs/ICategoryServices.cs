using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface ICategoryServices
    {
        public Task<List<Category>> GetCategoryList();
    }
}
