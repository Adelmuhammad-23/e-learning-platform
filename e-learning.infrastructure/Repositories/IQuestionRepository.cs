using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllAsync();
        Task<Question> GetByIdAsync(int id);
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}
