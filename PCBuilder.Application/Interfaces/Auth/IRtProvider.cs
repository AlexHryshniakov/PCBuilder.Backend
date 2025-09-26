using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Auth;

public interface IRtProvider
{
     string GenerateToken();
}