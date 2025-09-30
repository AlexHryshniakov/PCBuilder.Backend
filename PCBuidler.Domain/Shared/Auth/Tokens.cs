namespace PCBuidler.Domain.Shared.Auth;

public record Tokens(string AccessToken, string RefreshToken,DateTimeOffset RtExpiresAt);