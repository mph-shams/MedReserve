using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

public record UploadFileCommand(int AppointmentId, string FileName, byte[] Content, string ContentType) : IRequest<Result<int>>;

public class UploadFileHandler : IRequestHandler<UploadFileCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UploadFileHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(UploadFileCommand request, CancellationToken ct)
    {
        var file = new MedicalFile
        {
            AppointmentId = request.AppointmentId,
            FileName = request.FileName,
            FileContent = request.Content,
            ContentType = request.ContentType,
            Size = (ulong)request.Content.Length
        };

        await _unitOfWork.Repository<MedicalFile>().AddAsync(file);
        await _unitOfWork.SaveChangesAsync(ct);
        return Result<int>.Success(file.Id);
    }
}