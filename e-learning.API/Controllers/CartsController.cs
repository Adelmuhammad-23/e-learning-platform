using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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

    [HttpPost("{studentId}/add")]
    public async Task<IActionResult> AddToCart([FromHeader] string token, int studentId, int courseId)
    {
        var getStudent = await _studentServices.GetStudentAsync(studentId);
        if (getStudent == null)
            return NotFound("Student is not found !");
        var course = await _courseServices.GetCourseByIdAsync(courseId);
        if (course == null)
            return NotFound($"No course with this ID:{courseId} !");

        var accessToken = await _authenticationServices.ValidateToken(token);
        switch (accessToken)
        {
            case "InvalidToken":
                return Unauthorized("Token is not valid");
            case "NotExpired":
                {
                    var cart = await _cartService.AddToCartAsync(studentId, courseId);
                    return Ok(cart);
                }
            default:
                return BadRequest("Expired token");
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
        return Ok("Delete is successfully !+");
    }
















}

