using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;
        private readonly IStudentServices _studentServices;
        private readonly ICourseServices _courseServices;

        public EnrollmentController(IEnrollmentService service, IStudentServices studentServices, ICourseServices courseServices)
        {
            _service = service;
            _studentServices = studentServices;
            _courseServices = courseServices;
        }
        [HttpGet("Student/{studentId}")]
        public async Task<IActionResult> GetEnrollmentsForStudentAsync(int studentId)
        {
            var student = await _studentServices.GetStudentAsync(studentId);
            if (student == null)
                return NotFound($"Not found student with this Id:{studentId}");
            var enrollmentsForStudent = await _service.GetEnrollmentsForStudentAsync(studentId);
            if (enrollmentsForStudent == null)
                return NotFound("This student is not enrolled in any course.");
            return Ok(enrollmentsForStudent);
        }
        [HttpGet("Student/{studentId}/Course/{courseId}")]
        public async Task<IActionResult> GetEnrollmentsForStudentAsync(int studentId, int courseId)
        {
            var student = await _studentServices.GetStudentAsync(studentId);
            if (student == null)
                return NotFound($"Not found student with this Id:{studentId}");
            var course = await _courseServices.GetCourseByIdAsync(courseId);
            if (course == null)
                return NotFound($"Not found course with this Id:{courseId}");
            var studentisnrolled = await _service.isEnrollment(studentId, courseId);
            if (!studentisnrolled)
                return NotFound("This student is not enrolled in this course.");
            return Ok("already, this student is registered in this course.");
        }

    }
}
