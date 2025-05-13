using e_learning.API.Base;
using e_learning.Core.Features.Categories.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : AppControllerBase
    {
        [HttpGet()]

        public async Task<IActionResult> GetAllCategory() => NewResult(await Mediator.Send(new GetAllCategoryQuery()));
    }
}
