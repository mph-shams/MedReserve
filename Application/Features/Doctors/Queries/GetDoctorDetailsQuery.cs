using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace MedReserve.Application.Features.Doctors.Queries;

public record DoctorDetailsDto(int Id, string Specialty, string Bio, decimal Fee);
public record GetDoctorDetailsQuery(int Id) : IRequest<Result<DoctorDetailsDto>>;

public class GetDoctorDetailsHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetDoctorDetailsQuery, Result<DoctorDetailsDto>>
{
    public async Task<Result<DoctorDetailsDto>> Handle(GetDoctorDetailsQuery request, CancellationToken ct)
    {
        var doctor = await _unitOfWork.Repository<Domain.Entities.Doctor>().GetByIdAsync(request.Id);
        if (doctor == null) return Result<DoctorDetailsDto>.Failure("Doctor not Found!");
        return Result<DoctorDetailsDto>.Success(new DoctorDetailsDto(doctor.Id, doctor.Specialty, doctor.Bio, doctor.ConsultationFee));
    }
}