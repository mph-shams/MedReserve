using Application.Features.Appointments.Commands.UpdateAppointmentStatus;
using FluentValidation;

namespace MedReserve.Application.Features.Appointments.Commands.UpdateAppointmentStatus
{
    public class UpdateAppointmentStatusValidator : AbstractValidator<UpdateAppointmentStatusCommand>
    {
        public UpdateAppointmentStatusValidator()
        {

            RuleFor(x => x.Data.Status)
                .InclusiveBetween(0, 3)
                .WithMessage("The provided status is not valid!");

            RuleFor(x => x.Id).NotEmpty();
        }
    }
    
}
