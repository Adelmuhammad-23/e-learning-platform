using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getUserEnrolledCourses(int id)
        { 
            var CoursesEnrolled = await _service.GetEnrollmentsForStudentAsync(id);
            if(CoursesEnrolled == null) return NotFound("no courses Enrolled");
            return Ok(CoursesEnrolled);
        }
    }
}
