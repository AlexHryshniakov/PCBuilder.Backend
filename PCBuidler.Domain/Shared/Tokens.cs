namespace PCBuidler.Domain.Shared;

public record Tokens(string AccessToken, string RefreshToken,DateTimeOffset RtExpiresAt);