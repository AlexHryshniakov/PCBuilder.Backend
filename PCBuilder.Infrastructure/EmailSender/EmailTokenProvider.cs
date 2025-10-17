using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PCBuilder.Core.Shared.Email;
using PCBuilder.Application.Interfaces.Mail;

namespace PCBuilder.Infrastructure.EmailSender;

public class EmailTokenProvider(IOptions<EmailTokenOptions> emailToken):IEmailTokenProvider
{
    private readonly EmailTokenOptions _options = emailToken.Value;

    public string GenerateToken( Guid userId)
    {
        var payload = new
        {
            userId = userId.ToString(),
        };
    
        var payloadJson = JsonSerializer.Serialize(payload);
        var payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
        var payloadBase64 = Convert.ToBase64String(payloadBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.SecretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payloadBase64));
        var signature = Convert.ToBase64String(hash)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    
        return $"{payloadBase64}.{signature}";
    }

    
}