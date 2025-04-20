using e_learning.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Abstructs
{

    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructorsAsync();
        Task<Instructor?> GetInstructorByIdAsync(int id);
        Task AddInstructorAsync(Instructor instructor);

        Task<bool> UpdateInstructorAsync(int id, Instructor updatedInstructor, IFormFile ImageUrl);
        Task<bool> DeleteInstructorAsync(int id);
    }
}
