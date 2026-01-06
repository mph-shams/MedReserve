using FluentValidation;

namespace MedReserve.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Data.Username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Data.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Data.RoleId).InclusiveBetween(1, 3);
        }
    }
}