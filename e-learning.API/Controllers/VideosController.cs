using e_learning.API.Base;
using e_learning.Core.Features.Videos.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [ApiController]
    public class VideosController : AppControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> AddVideoInCourse([FromForm] AddVideoInCourse courseCommand) => NewResult(await Mediator.Send(courseCommand));

    }
}
