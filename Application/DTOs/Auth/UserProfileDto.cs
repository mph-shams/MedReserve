namespace MedReserve.Application.DTOs.Auth
{
    public record UserProfileDto(
        int Id,
        string Username,
        string Role,
        bool IsVerified,
        DateTime CreatedAt
    );
}
