using Domain.Enums;
using MediatR;
using MedReserve.Application.Features.Admin.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MedReserve.Application.Features.Admin.Commands;

namespace MedReserve.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")] 
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("reports")]
        public async Task<IActionResult> GetReports()
        {
            var result = await _mediator.Send(new GetSystemReportsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPatch("users/{userId}/role")]
        public async Task<IActionResult> UpdateRole(int userId, [FromBody] UserRole role)
        {
            var result = await _mediator.Send(new UpdateUserRoleCommand(userId, role));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-doctor/{id}")]
        public async Task<IActionResult> VerifyDoctor(int id)
        {
            var result = await _mediator.Send(new VerifyDoctorCommand(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
