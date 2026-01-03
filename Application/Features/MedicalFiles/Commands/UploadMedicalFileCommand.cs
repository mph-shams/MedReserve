using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.MedicalFiles.Commands;

public record UploadFileCommand(int AppointmentId, string FileName, byte[] Content, string ContentType) : IRequest<Result<int>>;

public class UploadFileHandler : IRequestHandler<UploadFileCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UploadFileHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UploadFileCommand request, CancellationToken ct)
    {
        if (request.Content.Length > 5 * 1024 * 1024)
            return Result<int>.Failure("File size cannot be larger than 5MB !");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
        var extension = Path.GetExtension(request.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
            return Result<int>.Failure("Only .jpg, .jpeg, .png, and .pdf formats are permitted");

        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(request.AppointmentId);
        if (appointment == null)
            return Result<int>.Failure("Appointment not found !");

        var file = new MedicalFile
        {
            AppointmentId = request.AppointmentId,
            FileName = request.FileName,
            FileContent = request.Content,
            ContentType = request.ContentType,
            Size = (ulong)request.Content.Length 
        };

        await _unitOfWork.Repository<MedicalFile>().AddAsync(file);

        var success = await _unitOfWork.SaveChangesAsync(ct) > 0;

        return success
            ? Result<int>.Success(file.Id)
            : Result<int>.Failure("Error saving file to the database !");
    }
}