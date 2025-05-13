using e_learning.Data.Helpers;
using e_learning.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize()]

    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }
        [HttpPatch("submit-score")]
        public async Task<IActionResult> SubmitQuizScore([FromQuery] int studentId, [FromQuery] int quizId, [FromQuery] double score)
        {

            await _quizService.SubmitQuizScoreAsync(studentId, quizId, score);
            return Ok(new { message = "Quiz score updated." });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _quizService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existingQuiz = await _quizService.GetByIdAsync(id);
            if (existingQuiz == null)
                return NotFound("Quiz not found");
            var result = await _quizService.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuizDto quiz)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _quizService.AddAsync(quiz);
            switch (result)
            {
                case "Course not found":
                    return NotFound("Course not found");
                case "Module not found":
                    return NotFound("Module not found");
                case "Course Added is successfully":
                    return Ok(new { message = "Quiz created successfully." });
                default:
                    return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateQuizDto quiz)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _quizService.UpdateAsync(id, quiz);
            switch (result)
            {
                case "Course not found":
                    return NotFound("Course not found");
                case "Module not found":
                    return NotFound("Module not found");
                case "Quiz not found":
                    return NotFound("Quiz not found");
                case "Course updated is successfully":
                    return Ok(new { message = "Quiz updated successfully." });
                default:
                    return BadRequest(result);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingQuiz = await _quizService.GetByIdAsync(id);
            if (existingQuiz == null)
                return NotFound("Quiz not found");
            await _quizService.DeleteAsync(id); return Ok();
        }

    }
}
