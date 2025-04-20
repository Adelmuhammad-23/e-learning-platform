using e_learning.API.Base;
using e_learning.Core.Features.Courses.Commands.Models;
using e_learning.Core.Features.Courses.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [ApiController]
    public class CoursesController : AppControllerBase
    {
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
        public async Task<IActionResult> AddCourse([FromForm] AddCourseCommand courseCommand) => NewResult(await Mediator.Send(courseCommand));

    }
}
