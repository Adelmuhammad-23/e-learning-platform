using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IInstructorRepository
    {
        Task<List<Instructor>> GetAllAsync();
        Task<List<Instructor>> GetInstructorsIsNotApproved();
        Task<Instructor?> GetByIdAsync(int id);
        Task AddAsync(Instructor instructor);
        Task<Instructor?> GetByEmailAsync(string email);

        Task UpdateAsync(Instructor instructor);
        Task DeleteAsync(Instructor instructor);
        Task<bool> ExistsAsync(int id);
    }
}
