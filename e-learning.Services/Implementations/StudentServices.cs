using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var stu = await _studentRepository.AddStudentAsync(student);
            if (stu != null)
                return stu;
            return "Can't add student because is null !";
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            var stu = await _studentRepository.GetStudentAsync(id);
            if (stu != null)
                return stu;
            return null;
        }
    }
}
