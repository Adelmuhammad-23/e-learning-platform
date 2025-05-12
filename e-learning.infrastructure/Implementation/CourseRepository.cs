using e_learning.Data.Entities;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.infrastructure.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        #endregion


        #region Functions
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            var courses = await _context.courses.AsNoTracking().ToListAsync();
            return courses;
        }
        public async Task<List<Course>> GetCoursesByCategoryIdAsync(int id)
        {
            var courses = await _context.courses.AsNoTracking().Where(c => c.CategoryId == id).ToListAsync();

            return courses;
        }

        public async Task<string> AddCourse(Course course)
        {
            await _context.courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return "AddSuccessfully";
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.courses.AnyAsync(m => m.Id == id);
        }
        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course = await _context.courses.Include(m => m.Modules).AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync();
            if (course == null)
                return null;
            return course;
        }

        public async Task<Course[]> GetCourseByInstructorIdAsync(int instructorId)
        {
            var course = await _context.courses.Include(m => m.Modules).AsNoTracking().Where(c => c.InstructorId == instructorId).ToArrayAsync();
            return course;
        }

        public async Task<string> DeleteCourseAsync(int courseId)
        {
            var course = await GetCourseByIdAsync(courseId);
            if (course != null)
            {
                _context.courses.Remove(course);
                await _context.SaveChangesAsync();
                return "Deleted";
            }
            return null;
        }
        #endregion

    }
} 
