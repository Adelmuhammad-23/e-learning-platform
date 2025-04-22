using e_learning.API.Base;
using e_learning.Core.Features.Review.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : AppControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddModuleAsync(AddReviewCommand reviewCommand) => NewResult(await Mediator.Send(reviewCommand));

    }
}
