using MedReserve.Application.Features.Schedules.Commands.CreateSchedule;
using Application.Features.Schedules.Queries.GetDoctorSchedules;
using MedReserve.Application.DTOs.Schedules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedReserve.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController(IMediator _mediator) : ControllerBase
{
    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetByDoctor(int doctorId)
    {
        var query = new GetDoctorSchedulesQuery { DoctorId = doctorId };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var command = new CreateScheduleCommand
        {
            UserId = int.Parse(userIdString),
            Data = request
        };

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}