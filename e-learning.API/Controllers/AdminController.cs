using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices, IInstructorService instructorService)
        {
            _instructorService = instructorService;
            _adminServices = adminServices;
        }
        [HttpGet("InstructorNotApproved")]
        public async Task<IActionResult> GetInstructorsApproved()
        {
            var instructors = await _instructorService.GetInstructorsIsNotApproved();
            if (instructors == null)
                return NotFound("Not instructor Not approved yet");
            return Ok(instructors);
        }
        [HttpPost("ApprovedInstructor/{id}")]
        public async Task<IActionResult> ApprovedInstructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
                return NotFound($"Not instructor Found with this id:{id}");
            var approved = await _adminServices.Approved(instructor);
            if (!approved)
                return BadRequest("Error when approved this instructor");

            return Ok("Approved this instructor is successfully");
        }
    }
}
