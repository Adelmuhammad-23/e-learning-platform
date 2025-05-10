using e_learning.Data.Entities;
using e_learning.Data.Helpers;
using Microsoft.AspNetCore.Http;

namespace e_learning.Services.Abstructs
{
    public interface IStudentServices
    {
        Task<Student> GetStudentAsync(int id);
        Task<string> AddStudentAsync(Student student);
        public Task<string> UpdateStudentAsync(int id, UpdateStudentDTO student, IFormFile ImageUrl);
    }
}
