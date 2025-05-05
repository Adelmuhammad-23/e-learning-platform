using e_learning.Data.Entities;

namespace e_learning.infrastructure.Repositories
{
    public interface ICourseRepository
    {
        public Task<Course> GetCourseByIdAsync(int courseId);
        public Task<string> DeleteCourseAsync(int courseId);
        public Task<Course[]> GetCourseByInstructorIdAsync(int instructorId);
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<List<Course>> GetCoursesByCategoryIdAsync(int id);
        Task<bool> ExistsAsync(int id);

        public Task<string> AddCourse(Course course);
    }
}
