using CareHomeApi.DTOs.Auth;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        var result = await _service.RegisterAsync(dto);
        return result is null ? Conflict("Username already taken.") : Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var result = await _service.LoginAsync(dto);
        return result is null ? Unauthorized("Invalid username or password.") : Ok(result);
    }
}
