using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _config;

    public IdentityService(IUnitOfWork uow, IConfiguration config)
    {
        _uow = uow;
        _config = config;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var userRepo = _uow.Repository<User>();
        var existingUser = (await userRepo.GetAllAsync()).FirstOrDefault(u => u.Username == request.Username);

        if (existingUser != null) return Result<AuthResponse>.Failure("A user with this username already exists");

        var user = new User
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = (UserRole)request.RoleId,
            IsVerified = true
        };

        await userRepo.AddAsync(user);
        await _uow.SaveChangesAsync();

        return await LoginAsync(new LoginRequest(request.Username, request.Password));
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = (await _uow.Repository<User>().GetAllAsync())
            .FirstOrDefault(u => u.Username == request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Result<AuthResponse>.Failure("The username or password is incorrect");

        var token = GenerateJwtToken(user);
        var refreshToken = Guid.NewGuid();

        var session = new Session
        {
            UserId = user.Id,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Device = "Unknown",
            IpAddress = "0.0.0.0"
        };

        await _uow.Repository<Session>().AddAsync(session);
        await _uow.SaveChangesAsync();

        return Result<AuthResponse>.Success(new AuthResponse(token, refreshToken, user.Username, user.Role.ToString()));
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
