using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment> GetEnrollmentAsync(int studentId, int courseId)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isEnrollment(int studentId, int courseId)
        {
            var check = await _context.Enrollments.Where(i => i.StudentId.Equals(studentId) && i.CourseId.Equals(courseId)).FirstOrDefaultAsync();
            if (check != null)
                return true;
            return false;
        }
    }

}
