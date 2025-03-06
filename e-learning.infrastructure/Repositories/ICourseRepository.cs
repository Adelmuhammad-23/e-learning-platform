using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<List<Course>> GetCoursesByCategoryIdAsync(int id);
    }
}
