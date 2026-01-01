using MediatR;
using Domain.Enums; 

namespace Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(
    Guid DoctorId,
    Guid PatientId,
    DateTime AppointmentDate,
    string Description
) : IRequest<Guid>;