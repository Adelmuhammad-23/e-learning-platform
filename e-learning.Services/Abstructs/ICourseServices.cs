using e_learning.Data.Entities;
using e_learning.Data.Entities.Views;

namespace e_learning.Services.Abstructs
{
    public interface ICourseServices
    {
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<List<Course>> GetCoursesByCategoryIdAsync(int id);
        public Task<List<TopPricedCourses>> GetTopPricedCourses();
    }
}
