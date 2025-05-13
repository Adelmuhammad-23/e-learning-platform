using e_learning.API.Base;
using e_learning.Core.Features.Categories.Queries.Models;
using e_learning.Data.Helpers;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : AppControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet()]

        public async Task<IActionResult> GetAllCategory() => NewResult(await Mediator.Send(new GetAllCategoryQuery()));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryServices.GetCategoryById(id);
            if (result == null) return NotFound($"Not found category with this Id:{id}");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var created = await _categoryServices.CreateCategory(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryServices.GetCategoryById(id);
            if (result == null) return NotFound($"Not found category with this Id:{id}");
            var updated = await _categoryServices.UpdateCategory(id, dto);
            if (!updated) return BadRequest();
            return Ok("Updated is successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryServices.GetCategoryById(id);
            if (result == null) return NotFound($"Not found category with this Id:{id}");
            var deleted = await _categoryServices.DeleteCategory(id);
            if (!deleted) return BadRequest();
            return Ok("Deleted is successfully");
        }
    }
}
