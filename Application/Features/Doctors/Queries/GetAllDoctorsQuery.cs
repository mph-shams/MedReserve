using Application.Common.Interfaces;
using Application.Common.Models;
using MedReserve.Application.DTOs.Doctors;
using MediatR;

namespace MedReserve.Application.Features.Doctors.Queries;

public record GetAllDoctorsQuery() : IRequest<Result<List<DoctorDto>>>;

public class GetAllDoctorsHandler : IRequestHandler<GetAllDoctorsQuery, Result<List<DoctorDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDoctorsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<DoctorDto>>> Handle(GetAllDoctorsQuery request, CancellationToken ct)
    {
        var doctors = (await _unitOfWork.Repository<Domain.Entities.Doctor>().GetAllAsync())
            .Select(d => new DoctorDto
            {Id = d.Id,Specialty = d.Specialty,ConsultationFee = d.ConsultationFee})
            .ToList();

        return Result<List<DoctorDto>>.Success(doctors);
    }
}