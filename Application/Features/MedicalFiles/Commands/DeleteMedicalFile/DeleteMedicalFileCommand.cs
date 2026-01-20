using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.MedicalFiles.Commands.DeleteMedicalFile;

public record DeleteMedicalFileCommand(int Id) : IRequest<Result<bool>>;

public class DeleteMedicalFileHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteMedicalFileCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteMedicalFileCommand request, CancellationToken ct)
    {
        var file = await _unitOfWork.Repository<Domain.Entities.MedicalFile>().GetByIdAsync(request.Id);
        if (file == null) return Result<bool>.Failure("File not found.");

        _unitOfWork.Repository<Domain.Entities.MedicalFile>().Delete(file);
        return await _unitOfWork.SaveChangesAsync(ct) > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("An error occurred while deleting the file.");
    }
}