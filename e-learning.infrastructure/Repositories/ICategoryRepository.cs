using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategory();
    }
}
