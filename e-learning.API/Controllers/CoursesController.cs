using e_learning.API.Base;
using e_learning.Core.Features.Courses.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [ApiController]
    public class CoursesController : AppControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetCourses()
        {
            return NewResult(await Mediator.Send(new GetAllCoursesQuery()));
        }
        [HttpGet("TopPricedCourses")]
        public async Task<IActionResult> GetTopPricedCourses()
        {
            return NewResult(await Mediator.Send(new GetTopPricedCoursesQuery()));
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCoursesByCategoryId([FromRoute] int categoryId)
        {
            return NewResult(await Mediator.Send(new GetAllCoursesByCategoryIdQuery(categoryId)));
        }
    }
}
