using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}