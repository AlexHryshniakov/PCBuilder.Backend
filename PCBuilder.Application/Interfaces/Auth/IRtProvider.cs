using PCBuilder.Core.Models;

namespace PCBuilder.Application.Interfaces.Auth;

public interface IRtProvider
{
     string GenerateToken();
}