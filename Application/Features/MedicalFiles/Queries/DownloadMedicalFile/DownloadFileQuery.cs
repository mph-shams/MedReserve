using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.MedicalFiles.Queries.DownloadMedicalFile;

public record DownloadFileResponse(byte[] Content, string ContentType, string FileName);

public record DownloadFileQuery(int Id) : IRequest<Result<DownloadFileResponse>>;

public class DownloadFileHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DownloadFileQuery, Result<DownloadFileResponse>>
{
    public async Task<Result<DownloadFileResponse>> Handle(DownloadFileQuery request, CancellationToken ct)
    {
        var file = await _unitOfWork.Repository<MedicalFile>().GetByIdAsync(request.Id);

        if (file == null)
            return Result<DownloadFileResponse>.Failure("File not found !");

        var response = new DownloadFileResponse(file.FileContent, file.ContentType, file.FileName);

        return Result<DownloadFileResponse>.Success(response);
    }
}