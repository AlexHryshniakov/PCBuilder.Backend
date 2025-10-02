using Microsoft.Extensions.Options;
using PCBuidler.Domain.Enums;
using PCBuilder.Application.Common.Extensions.StringExtensions;
using PCBuilder.Application.Interfaces.Mail;

namespace PCBuilder.Application.Services;

public class EmailService:IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplates _emailTemplates;
    private readonly string _baseUrl;

    public EmailService(IEmailSender emailSender,
        IEmailTemplates emailTemplates, IOptions<ApiSettings> apiOptions)
    {
        _emailSender = emailSender;
        _emailTemplates = emailTemplates;
        _baseUrl = apiOptions.Value.BaseUrl;
    }
    
    public async Task SendConfirmEmailAsync(string email,string token,Guid userId,CancellationToken ct)
    {
        string subject = "Confirm your email";
        
        var confirmationLink = $"{_baseUrl}/email/confirm?emailToken={token}";

        var body = _emailTemplates.
                        GetTemplate(EmailTemplateTypes.ConfirmEmail)
                        .FillPlaceholders(new Dictionary<EmailPlaceholders,string>
                        {
                            [EmailPlaceholders.ApiLink] = confirmationLink,
                        });
        await _emailSender.SendEmailAsync(email,subject, body);
    }
}
public class ApiSettings
{
    public string BaseUrl { get; set; } = null!;
}
