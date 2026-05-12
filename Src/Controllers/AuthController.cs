using Microsoft.AspNetCore.Mvc;
using ToDoListCSharp.Src.Models;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        AuthResponse? response = await authService.Login(request);

        if (response == null)
        {
            return Unauthorized();
        }

        return Ok(response);
    }
}