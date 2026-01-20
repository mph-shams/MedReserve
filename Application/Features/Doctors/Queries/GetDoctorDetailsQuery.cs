using Application.Common.Interfaces;
using Application.Common.Models;
using MedReserve.Application.DTOs.Doctors;
using MediatR;

namespace MedReserve.Application.Features.Doctors.Queries;

public record GetDoctorDetailsQuery(int Id) : IRequest<Result<DoctorDetailsDto>>;

public class GetDoctorDetailsHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetDoctorDetailsQuery, Result<DoctorDetailsDto>>
{
    public async Task<Result<DoctorDetailsDto>> Handle(GetDoctorDetailsQuery request, CancellationToken ct)
    {
        var doctor = await _unitOfWork.Repository<Domain.Entities.Doctor>().GetByIdAsync(request.Id);

        if (doctor == null)
            return Result<DoctorDetailsDto>.Failure("Doctor not Found!");

        return Result<DoctorDetailsDto>.Success(new DoctorDetailsDto
        {
            Id = doctor.Id,
            Specialty = doctor.Specialty,
            Bio = doctor.Bio,
            Fee = doctor.ConsultationFee
        });
    }
}