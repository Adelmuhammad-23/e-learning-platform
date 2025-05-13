using e_learning.API.Base;
using e_learning.Core.Features.Courses.Commands.Models;
using e_learning.Core.Features.Courses.Queries.Models;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [ApiController]
    public class CoursesController : AppControllerBase
    {
        private readonly IInstructorService _instructorService;

        public CoursesController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int id) => NewResult(await Mediator.Send(new GetCourseById(id)));

        [HttpGet("Instructor/{id}")]
        public async Task<IActionResult> GetCourseByInstructorId([FromRoute] int id) => NewResult(await Mediator.Send(new GetCourseByInstructorId(id)));

        [HttpGet()]
        public async Task<IActionResult> GetCourses() => NewResult(await Mediator.Send(new GetAllCoursesQuery()));
        [HttpGet("TopPricedCourses")]
        public async Task<IActionResult> GetTopPricedCourses() => NewResult(await Mediator.Send(new GetTopPricedCoursesQuery()));

        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetCoursesByCategoryId([FromRoute] int categoryId) => NewResult(await Mediator.Send(new GetAllCoursesByCategoryIdQuery(categoryId)));

        [HttpPost()]
        public async Task<IActionResult> AddCourse([FromForm] AddCourseCommand courseCommand)
        {
            var instructor = await _instructorService.GetInstructorByEmailAsync(courseCommand.InstructorEmail);
            if (instructor.isApproved == false)
                return Unauthorized("Not authorized to publish course please waiting for admin approval");
            return NewResult(await Mediator.Send(courseCommand));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id) => NewResult(await Mediator.Send(new DeleteCourseCommand(id)));

    }
}
