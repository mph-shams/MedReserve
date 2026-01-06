using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MedReserve.Application.DTOs.Doctors;
using MediatR;

namespace MedReserve.Application.Features.Doctors.Queries
{

    public record GetDoctorsBySpecialtyQuery(string Specialty) : IRequest<Result<List<DoctorDto>>>;

    public class GetDoctorsHandler : IRequestHandler<GetDoctorsBySpecialtyQuery, Result<List<DoctorDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDoctorsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Result<List<DoctorDto>>> Handle(GetDoctorsBySpecialtyQuery request, CancellationToken ct)
        {
            var doctors = (await _unitOfWork.Repository<Doctor>().GetAllAsync())
                .Where(d => d.Specialty == request.Specialty)
                .Select(d => new DoctorDto {Id = d.Id,Specialty = d.Specialty,ConsultationFee = d.ConsultationFee})
                .ToList(); ;

            return Result<List<DoctorDto>>.Success(doctors);
        }
    }
}
