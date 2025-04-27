using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface IStudentServices
    {
        Task<Student> GetStudentAsync(int id);
        Task<string> AddStudentAsync(Student student);
    }
}
