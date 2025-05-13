using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetByEmailAsync(string email);

        Task<Student> GetStudentAsync(int studentId);
        Task<string> AddStudentAsync(Student student);
        Task<string> UpdateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentAsync();

        Task<string> DeleteStudentAsync(Student student);
    }
}
