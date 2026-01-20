using Application.Common.Models;
using Application.DTOs.Auth;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
}