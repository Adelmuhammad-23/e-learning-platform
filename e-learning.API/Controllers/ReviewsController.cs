using e_learning.API.Base;
using e_learning.Core.Features.Review.Commands.Models;
using e_learning.Core.Features.Review.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : AppControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddModuleAsync(AddReviewCommand reviewCommand) => NewResult(await Mediator.Send(reviewCommand));


        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetCourseReviewsAsync(int courseId) =>
            NewResult(await Mediator.Send(new GetReviewsByCourseIdQuery(courseId)));

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewByIdAsync(int reviewId) =>
            NewResult(await Mediator.Send(new GetReviewByIdQuery(reviewId)));

        [HttpPut()]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] UpdateReviewCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }


        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId) =>
            NewResult(await Mediator.Send(new DeleteReviewCommand(reviewId)));
    }
}
