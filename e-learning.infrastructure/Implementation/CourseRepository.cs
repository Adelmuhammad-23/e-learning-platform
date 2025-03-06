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

        #endregion


    }
}
