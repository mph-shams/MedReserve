using FluentValidation;

namespace MedReserve.Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.AppointmentDate).GreaterThan(DateTime.Now).WithMessage("Appointment time must be in the future.");
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
        }
    }
}
