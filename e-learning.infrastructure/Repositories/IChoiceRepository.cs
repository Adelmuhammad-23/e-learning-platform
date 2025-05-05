using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IChoiceRepository
    {
        Task<List<Choice>> GetAllAsync();
        Task<Choice> GetByIdAsync(int id);
        Task AddAsync(Choice choice);
        Task UpdateAsync(Choice choice);
        Task DeleteAsync(int id);
    }
}
