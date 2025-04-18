using e_learning.Data.Entities;
using e_learning.Data.Entities.Views;
using e_learning.infrastructure.Repositories;
using e_learning.infrastructure.Repositories.Views;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Services.Implementations
{
    public class CourseServices : ICourseServices
    {
        #region Fields
        private readonly ICourseRepository _courseRepository;
        private readonly ITopPricedCoursesView<TopPricedCourses> _topPricedCourses;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;


        #endregion

        #region Constructors
        public CourseServices(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost, ITopPricedCoursesView<TopPricedCourses> topPricedCourses)
        {
            _courseRepository = courseRepository;
            _topPricedCourses = topPricedCourses;
            _httpContextAccessor = httpContextAccessor;
            _webHost = webHost;
        }

        #endregion

        #region Functions

        public async Task<string> AddCourse(Course course, IFormFile videoFile)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return "Failed to get request context";

            var webRootPath = _webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "images");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}{Path.GetExtension(videoFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/images/{uniqueFileName}";

            course.Image = fileUrl;
            await _courseRepository.AddCourse(course);

            return "Success";
        }
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
