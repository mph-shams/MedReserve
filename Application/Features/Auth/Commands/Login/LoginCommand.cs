using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public record LoginCommand(LoginRequest Data) : IRequest<Result<AuthResponse>>;

public class LoginHandler(IIdentityService _identityService) : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken ct)
    {
        return await _identityService.LoginAsync(request.Data);
    }
}