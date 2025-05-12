using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using e_learning.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly PayPalService _payPalService;
        private readonly IStudentRepository _studentRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IEnrollmentService _enrollmentService;


        public PaymentController(
            ICartService cartService,
            PayPalService payPalService,
            IStudentRepository studentRepository,
            ICartRepository cartRepository,
            IEnrollmentService enrollmentService)
        {
            _cartService = cartService;
            _payPalService = payPalService;
            _studentRepository = studentRepository;
            _cartRepository = cartRepository;
            _enrollmentService = enrollmentService;
        }


        [HttpGet("paypal/success")]
        public async Task<IActionResult> Success([FromQuery] string token, [FromQuery] int studentId)
        {
            var isCaptured = await _payPalService.CaptureOrderIfApprovedAsync(token);
            if (!isCaptured)
                return BadRequest("Payment failed");

            var cart = await _cartRepository.GetCartAsync(studentId);
            if (cart != null)
            {
                foreach (var course in cart.Courses)
                {
                    await _enrollmentService.EnrollStudentInCourseAsync(studentId, course.CourseId);
                }
                await _cartRepository.GetCartAsync(studentId);
            }
            await _cartRepository.DeleteCartAsync(studentId);
            return Ok("Payment completed and courses assigned.");
        }


        [HttpGet("paypal/cancel")]
        public IActionResult Cancel()
        {
            return Ok("Payment cancelled.");
        }
    }

}
