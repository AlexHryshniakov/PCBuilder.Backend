namespace PCBuidler.Domain;

public record Tokens(string AccessToken, string RefreshToken,DateTimeOffset RtExpiresAt);