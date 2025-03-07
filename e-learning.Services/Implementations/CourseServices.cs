using e_learning.Data.Entities;
using e_learning.Data.Entities.Views;
using e_learning.infrastructure.Repositories;
using e_learning.infrastructure.Repositories.Views;
using e_learning.Services.Abstructs;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Services.Implementations
{
    public class CourseServices : ICourseServices
    {
        #region Fields
        private readonly ICourseRepository _courseRepository;
        private readonly ITopPricedCoursesView<TopPricedCourses> _topPricedCourses;


        #endregion

        #region Constructors
        public CourseServices(ICourseRepository courseRepository, ITopPricedCoursesView<TopPricedCourses> topPricedCourses)
        {
            _courseRepository = courseRepository;
            _topPricedCourses = topPricedCourses;
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

        public async Task<List<TopPricedCourses>> GetTopPricedCourses()
        {
            return await _topPricedCourses.GetTableNoTracking().ToListAsync();
        }

        #endregion
    }
}
