using PCBuilder.Application.Interfaces.Auth;
using System.Security.Cryptography; 

namespace PCBuilder.Infrastructure.Authentication;

public class RtProvider : IRtProvider
{
    public string GenerateToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}