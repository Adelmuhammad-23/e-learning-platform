using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;

namespace e_learning.Services.Implementations
{
    public class CourseServices : ICourseServices
    {
        #region Fields
        private readonly ICourseRepository _courseRepository;


        #endregion

        #region Constructors
        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        #endregion

        #region Functions
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }
        public async Task<List<Course>> GetCoursesByCategoryIdAsync(int id)
        {
            return await _courseRepository.GetCoursesByCategoryIdAsync(id);
        }

        #endregion
    }
}
