using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Features.Appointments.Queries.GetPatientAppointments;

public record PatientAppointmentDto(int Id, int DoctorId, DateTime Date, string Status);
public record GetPatientAppointmentsQuery(int PatientId) : IRequest<Result<List<PatientAppointmentDto>>>;

public class GetPatientAppointmentsHandler : IRequestHandler<GetPatientAppointmentsQuery, Result<List<PatientAppointmentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPatientAppointmentsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<List<PatientAppointmentDto>>> Handle(GetPatientAppointmentsQuery request, CancellationToken ct)
    {
        var appointments = (await _unitOfWork.Repository<Appointment>().GetAllAsync())
            .Where(a => a.PatientId == request.PatientId)
            .Select(a => new PatientAppointmentDto(a.Id, a.DoctorId, a.AppointmentDate, a.Status.ToString()))
            .ToList();
        return Result<List<PatientAppointmentDto>>.Success(appointments);
    }
}