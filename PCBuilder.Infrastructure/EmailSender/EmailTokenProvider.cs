using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PCBuilder.Application.Interfaces.Auth;
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
            expiry = DateTimeOffset.UtcNow.Add(_options.Lifetime).ToUnixTimeSeconds()
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

    public Guid? VerifyToken(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 2) return null;

        var payloadBase64 = parts[0];
        var signature = parts[1];

        // Добавляем padding '=' для правильного декодирования
        var signatureBase64Padded = signature.Replace('-', '+').Replace('_', '/');
        switch (signatureBase64Padded.Length % 4)
        {
            case 2: signatureBase64Padded += "=="; break;
            case 3: signatureBase64Padded += "="; break;
        }
    
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.SecretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payloadBase64));
        var calculatedSignature = Convert.ToBase64String(hash)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

        if (signature != calculatedSignature) return null;
        
        var payloadJson = Encoding.UTF8.GetString(Convert.FromBase64String(payloadBase64));
        var payload = JsonSerializer.Deserialize<dynamic>(payloadJson);

        var expiry = (long)payload!.GetProperty("expiry").GetInt64();
        if (DateTimeOffset.FromUnixTimeSeconds(expiry) < DateTimeOffset.UtcNow)
        {
            return null; 
        }
        
        return Guid.Parse(payload.GetProperty("userId").GetString());
    }
    
}