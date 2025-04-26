using e_learning.Services.Abstructs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IAuthenticationServices _authenticationServices;


    public CartController(IAuthenticationServices authenticationServices, ICartService cartService)
    {
        _cartService = cartService;
        _authenticationServices = authenticationServices;

    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetCart(int studentId)
    {
        var cart = await _cartService.GetCartAsync(studentId);
        return Ok(cart);
    }

    [HttpPost("{studentId}/add")]
    public async Task<IActionResult> AddToCart([FromHeader] string token, int studentId, int courseId)
    {

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
        await _cartService.RemoveFromCartAsync(studentId, courseId);
        return Ok("Delete is successfully !+");
    }
}
