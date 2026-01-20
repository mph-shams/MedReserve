using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace MedReserve.Application.Features.Admin.Commands
{
    public record VerifyDoctorCommand(int DoctorId) : IRequest<Result<bool>>;

    public class VerifyDoctorHandler(IUnitOfWork _unitOfWork) : IRequestHandler<VerifyDoctorCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(VerifyDoctorCommand request, CancellationToken ct)
        {
            var doctor = await _unitOfWork.Repository<Domain.Entities.Doctor>().GetByIdAsync(request.DoctorId);

            if (doctor == null)
                return Result<bool>.Failure("The requested doctor was not found.");

            return Result<bool>.Success(true);
        }
    }
}
