using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Infrastructure.Authentication;

namespace PCBuilder.Infrastructure.EmailSender;

public class SmtpEmailSender(IOptions<SmtpOptions> options) : IEmailSender
{
    private readonly SmtpOptions _options=options.Value;
    
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_options.FromAddress),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        using var client = new SmtpClient(_options.Host, _options.Port);
        
        client.Credentials = new NetworkCredential(_options.FromAddress, _options.Password);
        client.EnableSsl = _options.UseSsl;

        await client.SendMailAsync(mailMessage);
    }
}