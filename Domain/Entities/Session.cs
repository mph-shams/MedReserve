using Domain.Common;

namespace Domain.Entities;

public class Session : BaseEntity
{
    public int UserId { get; set; }
    public Guid RefreshToken { get; set; }
    public string Device { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}