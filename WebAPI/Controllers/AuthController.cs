using Application.DTOs.Auth;
using Application.Features.Auth.Commands.Login;
using MediatR;
using MedReserve.Application.DTOs.Auth;
using MedReserve.Application.Features.Auth.Commands.ChangePassword;
using MedReserve.Application.Features.Auth.Commands.Register;
using MedReserve.Application.Features.Auth.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; 

namespace MedReserve.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator _mediator) : ControllerBase 
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
   
        var result = await _mediator.Send(new RegisterCommand(request));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _mediator.Send(new LoginCommand(request));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(new ChangePasswordCommand(userId, request));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _mediator.Send(new GetCurrentUserQuery(userId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}