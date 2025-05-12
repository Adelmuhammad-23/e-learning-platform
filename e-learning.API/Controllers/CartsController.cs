using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICourseServices _courseServices;
    private readonly IStudentServices _studentServices;
    private readonly IAuthenticationServices _authenticationServices;



    public CartController(ICartService cartService, ICourseServices courseServices, IStudentServices studentServices, IAuthenticationServices authenticationServices)
    {
        _cartService = cartService;
        _courseServices = courseServices;
        _studentServices = studentServices;
        _authenticationServices = authenticationServices;

    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetCart(int studentId)
    {
        var getStudent = await _studentServices.GetStudentAsync(studentId);
        if (getStudent == null)
            return NotFound("Student is not found !");

        var cart = await _cartService.GetCartAsync(studentId);
        return Ok(cart);
    }

    [HttpPost("add/{courseId}")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest studentId,int courseId)
    {
        var getStudent = await _studentServices.GetStudentAsync(studentId.StudentId);
        if (getStudent == null)
            return NotFound("Student is not found !");
        var course = await _courseServices.GetCourseByIdAsync(courseId);
        if (course == null)
            return NotFound($"No course with this ID:{courseId} !");

        var accessToken = Request.Headers["Authorization"];
        var token = accessToken.ToString().Replace("Bearer ", "");
        var validateToken = await _authenticationServices.ValidateToken(token);
        switch (validateToken)
        {
            case "InvalidToken":
                return Unauthorized("Token is not valid");
            case "NotExpired":
                {
                    var cart = await _cartService.AddToCartAsync(studentId.StudentId, courseId);
                    return Ok(cart);
                }
            default:
                return BadRequest("Expired token or another reason to failed in this token ");
        }
    }

    [HttpDelete("{studentId}/remove/{courseId}")]
    public async Task<IActionResult> RemoveFromCart(int studentId, int courseId)
    {
        var getStudent = await _studentServices.GetStudentAsync(studentId);
        if (getStudent == null)
            return NotFound("Student is not found !");
        var course = await _courseServices.GetCourseByIdAsync(courseId);
        if (course == null)
            return NotFound($"No course with this ID:{courseId} !");
        await _cartService.RemoveFromCartAsync(studentId, courseId);
        return Ok(new { message = "Delete is done" });
    }


    [HttpPost("checkout/{studentId}")]
    public async Task<IActionResult> Checkout(int studentId)
    {
        var url = await _cartService.CheckoutAsync(studentId);
        return Ok(new { Url = url });
    }


    public class AddToCartRequest
    {
        public int StudentId { get; set; }
    }



}


