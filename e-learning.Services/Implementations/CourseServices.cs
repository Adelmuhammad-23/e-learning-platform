using e_learning.Data.Entities;
using e_learning.Data.Entities.Identity;
using e_learning.Data.Entities.Views;
using e_learning.infrastructure.Repositories;
using e_learning.infrastructure.Repositories.Views;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Services.Implementations
{
    public class CourseServices : ICourseServices
    {
        #region Fields
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;
        private readonly ITopPricedCoursesView<TopPricedCourses> _topPricedCourses;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHost;


        #endregion

        #region Constructors
        public CourseServices(ICourseRepository courseRepository, IInstructorRepository instructorRepository, RoleManager<Role> roleManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost, ITopPricedCoursesView<TopPricedCourses> topPricedCourses)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _userManager = userManager;
            _roleManager = roleManager;
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

            var inst = await _instructorRepository.GetByEmailAsync(course.InstructorEmail);
            if (inst == null)
                return "Not Authorized because Instructor Not Found";
            course.InstructorId = inst.Id;

            var webRootPath = _webHost.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads", "images", "CourseImages");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}{Path.GetExtension(videoFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/images/CourseImages/{uniqueFileName}";

            course.Image = fileUrl;
            await _courseRepository.AddCourse(course);

            return "Success";
        }

        public async Task<string> DeleteCourseAsync(int courseId)
        {
            var courseDeleted = await _courseRepository.DeleteCourseAsync(courseId);
            return courseDeleted;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var couersse = await _courseRepository.ExistsAsync(id);
            if (!couersse) return false;
            return couersse;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
                return null;
            return course;
        }

        public async Task<Course[]> GetCourseByInstructorIdAsync(int instructorId)
        {
            var course = await _courseRepository.GetCourseByInstructorIdAsync(instructorId);
            if (course.Length == 0)
                return null;
            return course;
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
