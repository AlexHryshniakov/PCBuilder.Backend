using PCBuilder.Application.Interfaces.Auth;

namespace PCBuilder.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
   

    public bool Verify(string password, string hashedPassword)
    => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}