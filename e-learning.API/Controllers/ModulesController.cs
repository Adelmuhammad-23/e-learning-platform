using e_learning.API.Base;
using e_learning.Core.Features.Modules.Commands.Models;
using e_learning.Core.Features.Modules.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : AppControllerBase
    {
        [HttpGet("Course/{id}")]
        public async Task<IActionResult> GetModulesInCourseAsync([FromRoute] int id) => NewResult(await Mediator.Send(new GetByCourseIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> AddModuleAsync(AddModuleCommand moduleCommand) => NewResult(await Mediator.Send(moduleCommand));

        [HttpPut]
        public async Task<IActionResult> UpdateModuleAsync(UpdateModuleCommand moduleCommand) => NewResult(await Mediator.Send(moduleCommand));

        [HttpPost("video")]
        public async Task<IActionResult> AddVideoToModuleAsync(AddVideoToModuleCommand moduleCommand) => NewResult(await Mediator.Send(moduleCommand));

        [HttpDelete("video")]
        public async Task<IActionResult> DeleteVideoFromModuleAsync(DeleteVideoFromModuleCommand moduleCommand) => NewResult(await Mediator.Send(moduleCommand));

    }
}
