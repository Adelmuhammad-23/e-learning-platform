using e_learning.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Abstructs
{

    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructorsAsync();
        Task<Instructor?> GetInstructorByIdAsync(int id);
        Task<Instructor?> GetInstructorByEmailAsync(string email);
        Task AddInstructorAsync(Instructor instructor);

        Task<bool> UpdateInstructorAsync(int id, Instructor updatedInstructor, IFormFile ImageUrl);
        Task<bool> DeleteInstructorAsync(int id);

        Task<bool> isInstrucorCourse(int InstructorId, int CourseId);
    }
}
