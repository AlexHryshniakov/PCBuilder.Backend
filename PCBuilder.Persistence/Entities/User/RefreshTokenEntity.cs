namespace PCBuilder.Persistence.Entities.User;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }=String.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
}