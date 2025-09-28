namespace PCBuidler.Domain.Models;

public record Tokens(string AccessToken, string RefreshToken,DateTimeOffset RtExpiresAt);