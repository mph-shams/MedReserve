using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedReserve.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;
    public AuthController(IIdentityService identityService) => _identityService = identityService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request) => Ok(await _identityService.RegisterAsync(request));

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request) => Ok(await _identityService.LoginAsync(request));

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] string newPassword)
    {
        return Ok("Password changed successfully!");
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        return Ok("Logged-in user information!");
    }
}