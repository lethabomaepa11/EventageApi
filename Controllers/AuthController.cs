

using EventAgeAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace EventAgeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    // This controller will handle authentication-related actions such as registration and login.
    // You can implement methods for user registration, login, and token generation here.

    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {


        var result = await _authService.RegisterAsync(registerDto);
        if (result.StartsWith("Registration failed"))
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        if (result is null)
        {
            return Unauthorized("Login failed: Invalid email or password.");
        }
        return Ok(result);
    }
}
