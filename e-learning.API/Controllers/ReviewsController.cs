using e_learning.API.Base;
using e_learning.Core.Features.Review.Commands.Models;
using e_learning.Core.Features.Review.Queries.Models;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]

    public class ReviewsController : AppControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;

        public ReviewsController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddModuleAsync([FromHeader] string Token, AddReviewCommand reviewCommand)
        {
            var token = await _authenticationServices.ValidateToken(Token);
            switch (token)
            {
                case "InvalidToken":
                    return Unauthorized("Token is not valid");
                case "NotExpired":
                    {
                        return NewResult(await Mediator.Send(reviewCommand));

                    }
                default:
                    return BadRequest("error when check token is valid or not");
            }
        }


        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetCourseReviewsAsync(int courseId) =>
            NewResult(await Mediator.Send(new GetReviewsByCourseIdQuery(courseId)));

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewByIdAsync(int reviewId) =>
            NewResult(await Mediator.Send(new GetReviewByIdQuery(reviewId)));

        [HttpPut()]
        public async Task<IActionResult> UpdateReviewAsync([FromHeader] string Token, [FromBody] UpdateReviewCommand command)
        {
            var token = await _authenticationServices.ValidateToken(Token);
            switch (token)
            {
                case "InvalidToken":
                    return Unauthorized("Token is not valid");
                case "NotExpired":
                    {
                        return NewResult(await Mediator.Send(command));
                    }
                default:
                    return BadRequest("Error validating token");
            }
        }


        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId) =>
            NewResult(await Mediator.Send(new DeleteReviewCommand(reviewId)));

    }
}
