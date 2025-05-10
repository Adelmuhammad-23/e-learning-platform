using e_learning.Data.Helpers;
using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public StudentsController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] int id, [FromForm] UpdateStudentDTO updateStudent)
        {
            var studentResult = await _studentServices.UpdateStudentAsync(id, updateStudent, updateStudent.Image);
            switch (studentResult)
            {
                case "NotFound":
                    return NotFound("student not found");
                case "error in httpContext":
                    return BadRequest("error in httpContext");
                case "updated":
                    return Ok("Update student is Successfully");
                default:
                    return BadRequest(new { message = $" {studentResult.ToString()} !!" });
            }
        }
    }
}
