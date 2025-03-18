using e_learning.API.Base;
using e_learning.Core.Features.Authentication.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand) =>
            NewResult(await Mediator.Send(registerCommand));
    }
}
