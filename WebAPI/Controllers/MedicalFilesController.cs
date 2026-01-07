using Application.Features.MedicalFiles.Commands;
using Application.Features.MedicalFiles.Commands.DeleteMedicalFile;
using Application.Features.MedicalFiles.Queries.DownloadMedicalFile;
using Application.Features.MedicalFiles.Queries.GetMedicalFilesByAppointment;
using MediatR;
using MedReserve.Application.Features.MedicalFiles.Commands.UploadMedicalFileCommand;
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

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        var result = await _mediator.Send(new GetMedicalFilesByAppointmentQuery(appointmentId));
        return Ok("List of all files attached to this appointment!");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] int appointmentId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file has been selected !");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var command = new UploadFileCommand(
            appointmentId,
            file.FileName,
            memoryStream.ToArray(),
            file.ContentType
        );

        var result = await _mediator.Send(command);
        return Ok("File uploaded successfully !");
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        var result = await _mediator.Send(new DownloadFileQuery(id));

        if (!result.IsSuccess)
            return BadRequest(result);

        return File(result.Value.Content, result.Value.ContentType, result.Value.FileName);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteMedicalFileCommand(id));
        return Ok("File deleted successfully!");
    }


}