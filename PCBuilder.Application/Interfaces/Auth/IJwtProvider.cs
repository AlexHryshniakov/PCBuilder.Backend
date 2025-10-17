using PCBuilder.Core.Models;

namespace PCBuilder.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}