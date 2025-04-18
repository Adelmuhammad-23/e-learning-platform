using e_learning.API.Base;
using e_learning.Core.Features.Modules.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddModuleAsync(AddModuleCommand moduleCommand) => NewResult(await Mediator.Send(moduleCommand));

    }
}
