using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Services.Abstructs
{
    public interface IStudentServices
    {
        Task<StudentDTO> GetStudentAsync(int id);
        Task<List<StudentDTO>> GetAllStudentAsync();
        Task<string> AddStudentAsync(Student student);
        Task<string> DeleteStudentAsync(int studentId);
        public Task<string> UpdateStudentAsync(int id, UpdateStudentDTO student, IFormFile ImageUrl);

        Task<IActionResult> GetAllStudent();
    }
}
