using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;


namespace MedReserve.Application.Features.Admin.Queries
{
    
    public record SystemReportDto(int TotalAppointments, int DoneAppointments, int TotalDoctors, decimal TotalRevenue);
    public record GetSystemReportsQuery() : IRequest<Result<SystemReportDto>>;

    public class GetSystemReportsHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetSystemReportsQuery, Result<SystemReportDto>>
    {
        public async Task<Result<SystemReportDto>> Handle(GetSystemReportsQuery request, CancellationToken ct)
        {
            var appointments = await _unitOfWork.Repository<Appointment>().GetAllAsync(); 
            var doctorsCount = (await _unitOfWork.Repository<Doctor>().GetAllAsync()).Count();

            var report = new SystemReportDto(
                TotalAppointments: appointments.Count(),
                DoneAppointments: appointments.Count(a => a.Status == AppointmentStatus.Done),
                TotalDoctors: doctorsCount,
                TotalRevenue: appointments.Where(a => a.Status == AppointmentStatus.Done).Sum(a => 50000) 
            );

            return Result<SystemReportDto>.Success(report);
        }
    }
}
