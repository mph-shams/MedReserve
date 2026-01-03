using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedReserve.WebAPI.Controllers
{
    [Authorize]
    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(int id)
    {
        var file = await _unitOfWork.Repository<MedicalFile>().GetByIdAsync(id);
        if (file == null) return NotFound(Result<string>.Failure("File not Found! "));

        var stream = new MemoryStream(file.FileContent);
        return new FileStreamResult(stream, file.ContentType)
        {
            FileDownloadName = file.FileName
        };
    }
}
