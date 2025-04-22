using e_learning.API.Base;
using e_learning.Core.Features.Authentication.Commands.Models;
using e_learning.Core.Features.Authentication.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand) =>
            NewResult(await Mediator.Send(registerCommand));

        [HttpPost("Login")]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost("SendResetPassword")]
        public async Task<IActionResult> SendResetPassword([FromForm] SendResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
