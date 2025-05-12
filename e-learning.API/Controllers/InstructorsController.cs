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
        [Authorize(Roles = "Instructor,Student")]

        public async Task<IActionResult> GetAllInstructorsAsync() =>
           NewResult(await Mediator.Send(new GetAllInstructorsQuery()));

        [HttpGet("{id}")]
        [Authorize(Roles = "Instructor,Student")]
        public async Task<IActionResult> GetInstructorByIdAsync([FromRoute] int id) =>
            NewResult(await Mediator.Send(new GetInstructorByIdQuery(id)));

        [HttpPut]
        public async Task<IActionResult> UpdateInstructorAsync([FromForm] UpdateInstructorCommand command) =>
            NewResult(await Mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructorAsync([FromRoute] int id) =>
            NewResult(await Mediator.Send(new DeleteInstructorCommand { Id = id }));
    }
}
