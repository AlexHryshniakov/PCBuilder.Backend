using PCBuilder.Core.Models.Users;

namespace PCBuilder.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}