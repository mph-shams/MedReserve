using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.Auth;
using MediatR;

namespace MedReserve.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(RegisterRequest Data) : IRequest<Result<AuthResponse>>;

    public class RegisterHandler(IIdentityService _identityService) : IRequestHandler<RegisterCommand, Result<AuthResponse>>
    {
        public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken ct)
        {
            return await _identityService.RegisterAsync(request.Data);
        }
    }
}
