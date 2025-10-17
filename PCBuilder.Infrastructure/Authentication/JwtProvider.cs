using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PCBuilder.Core.Shared.Auth;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Core.Models.Users;

namespace PCBuilder.Infrastructure.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new (CustomClaims.UserId, user.Id.ToString()),
        ];
        
        var signingCredentials= new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes),
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}