using MediatR;
using Domain.Enums; 

namespace Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(
    int DoctorId,
    int PatientId,
    DateTime AppointmentDate,
    string Description
) : IRequest<int>;