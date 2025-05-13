using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student?> GetByEmailAsync(string Email)
        {
            return await _context.Students.AsNoTracking()
                .FirstOrDefaultAsync(i => i.Email == Email);
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var stu = await _context.Students.AddAsync(student);
            return "AddSuccessfully";
        }

        public async Task<Student> GetStudentAsync(int studentId)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id.Equals(studentId));
        }
        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }
        public async Task<string> UpdateStudentAsync(Student student)
        {
            var stu = _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return "updated";
        }

        public async Task<string> DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return "Deleted";
        }

    }
}
