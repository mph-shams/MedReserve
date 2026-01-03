using Application.Common.Models;
using Application.Features.MedicalFiles.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MedicalFilesController : ControllerBase
{
    private readonly IMediator _mediator;
    public MedicalFilesController(IMediator mediator) => _mediator = mediator;

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] int appointmentId, IFormFile file)
    {
        return Ok("File uploaded successfully !");
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        return Ok("Downloading file...");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // var result = await _mediator.Send(new DeleteMedicalFileCommand(id));
        return Ok("File deleted successfully!");
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        // var result = await _mediator.Send(new GetMedicalFilesByAppointmentQuery(appointmentId));
        return Ok("List of all files attached to this appointment!");
    }
}