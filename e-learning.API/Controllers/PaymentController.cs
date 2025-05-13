using e_learning.infrastructure.Repositories;
using e_learning.Services.Abstructs;
using e_learning.Services.Implementations;
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
        private readonly IEmailServices _emailServices;




        public PaymentController(
            IEmailServices emailServices,
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
            _emailServices = emailServices;
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

                // ✅ Get student email (assuming you have a StudentService or similar)
                var student = await _studentRepository.GetStudentAsync(studentId);
                if (student != null)
                {
                    #region Purchase Confirmation Email Template
                    var message = $@"
        <html>
        <head>
            <style>
                .email-container {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                    text-align: center;
                }}
                .email-box {{
                    background: white;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                    display: inline-block;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 12px;
                    color: #777;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <div class='email-box'>
                    <h2>Course Purchase Confirmation</h2>
                    <p>Thank you for purchasing courses on <strong>Xcelerate Platform</strong>!</p>
                    <p>You now have access to your new courses. Happy learning! 🎉</p>
                    <p class='footer'>If you have any questions, feel free to contact our support.</p>
                </div>
            </div>
        </body>
        </html>";
                    #endregion

                    await _emailServices.SendEmailAsync(student.Email, message, "Course Purchase Confirmation");
                }
            }
            await _cartRepository.DeleteCartAsync(studentId);
            return Redirect("http://localhost:4200/home");
        }


        [HttpGet("paypal/cancel")]
        public IActionResult Cancel()
        {
            return Ok("Payment cancelled.");
        }
    }

}
