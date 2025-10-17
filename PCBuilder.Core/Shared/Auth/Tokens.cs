namespace PCBuilder.Core.Shared.Auth;

public record Tokens(string AccessToken, string RefreshToken,DateTimeOffset RtExpiresAt);