using e_learning.Data.Entities;

namespace e_learning.Services.Abstructs
{
    public interface ICourseServices
    {
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<List<Course>> GetCoursesByCategoryIdAsync(int id);
    }
}
