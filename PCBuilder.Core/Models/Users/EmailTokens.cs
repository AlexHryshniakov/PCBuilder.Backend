namespace PCBuilder.Core.Models.Users;

public class EmailTokens
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    public string ConfirmEmailToken { get; set; } = String.Empty;
    public DateTimeOffset ConfirmEmailExpiresAt { get; set; }
    
    public string PasswordResetToken { get; set; } = String.Empty;
    public DateTimeOffset PasswordResetExpiresAt { get; set; }
    public bool PasswordResetIsAllowed {get; set;}
}