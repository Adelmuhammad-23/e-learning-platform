using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IModuleRepository
    {
        Task<Module> GetByIdAsync(int id);
        Task<List<Module>> GetByCourseIdAsync(int courseId);
        Task AddAsync(Module module);
        Task DeleteAsync(int id);
        Task UpdateAsync(Module module);
        Task<bool> ExistsAsync(int id);
    }
}
