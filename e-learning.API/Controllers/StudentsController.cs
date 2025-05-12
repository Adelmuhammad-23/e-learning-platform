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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllStudent([FromRoute] int id)
        {
            var student = await _studentServices.GetStudentAsync(id);
            if (student == null)
                return NotFound($"Not found student with this Id: {id}");
            return Ok(student);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var students = await _studentServices.GetAllStudentAsync();
            if (students == null)
                return NotFound("Not found students yet");
            return Ok(students);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await _studentServices.DeleteStudentAsync(id);
            switch (student)
            {
                case "NotFound":
                    return NotFound($"Not found student with this Id: {id}");
                case "Deleted":
                    return Ok("Student is deleted successfully");
                default:
                    return BadRequest("Error when delete student");
            }
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
