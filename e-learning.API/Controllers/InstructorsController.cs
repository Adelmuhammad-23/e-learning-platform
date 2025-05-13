using e_learning.API.Base;
using e_learning.Core.Features.Instructors.Commands.Models;
using e_learning.Core.Features.Instructors.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Instructor,Student")]

    public class InstructorsController : AppControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllInstructorsAsync() =>
           NewResult(await Mediator.Send(new GetAllInstructorsQuery()));

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetInstructorByIdAsync([FromRoute] int id) =>
            NewResult(await Mediator.Send(new GetInstructorByIdQuery(id)));

        [HttpPut]
        public async Task<IActionResult> UpdateInstructorAsync([FromForm] UpdateInstructorCommand command) =>
            NewResult(await Mediator.Send(command));

        [HttpPatch()]
        public async Task<IActionResult> AddProfessionalInstructorAsync([FromForm] AddProfessionalInstructorCommand command) =>
            NewResult(await Mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructorAsync([FromRoute] int id) =>
            NewResult(await Mediator.Send(new DeleteInstructorCommand { Id = id }));
    }
}
