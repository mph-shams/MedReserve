using MediatR;
using Application.Common.Models;

namespace MedReserve.Application.Features.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(int DoctorId, int PatientId, DateTime AppointmentDate, string Description) : IRequest<Result<int>>;