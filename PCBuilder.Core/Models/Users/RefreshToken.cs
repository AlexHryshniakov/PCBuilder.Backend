namespace PCBuilder.Core.Models.Users;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}