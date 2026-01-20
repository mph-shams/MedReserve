namespace Application.DTOs.Auth;

public record RegisterRequest(string Username, string Password, int RoleId, long? TelegramChatId = null);
public record LoginRequest(string Username, string Password);
public record AuthResponse(string AccessToken, Guid RefreshToken, string Username, string Role);