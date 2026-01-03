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

    public MedicalFilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] int appointmentId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(Result<string>.Failure("No file selected!"));

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var command = new UploadFileCommand(
            appointmentId,
            file.FileName,
            memoryStream.ToArray(),
            file.ContentType
        );

        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        return Ok("GetFileQuery method: Ready for implementation!");
    }
}