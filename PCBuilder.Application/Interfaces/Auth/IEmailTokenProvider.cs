namespace PCBuilder.Application.Interfaces.Auth;

public interface IEmailTokenProvider
{
    string GenerateToken(Guid userId);
    Guid? VerifyToken(string token);
}