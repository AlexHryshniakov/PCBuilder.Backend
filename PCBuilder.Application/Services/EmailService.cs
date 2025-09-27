using Microsoft.Extensions.Options;
using PCBuidler.Domain.Enums;
using PCBuilder.Application.Common.Extensions.StringExtensions;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Mail;

namespace PCBuilder.Application.Services;

public class EmailService(
    IEmailSender emailSender,
    IEmailTokenProvider tokenProvider,
    IEmailTemplates emailTemplates,
    IOptions<ApiSettings> apiOptions):IEmailService
{
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IEmailTokenProvider _tokenProvider = tokenProvider;
    private readonly IEmailTemplates _emailTemplates = emailTemplates;
    private readonly string _baseUrl = apiOptions.Value.BaseUrl;
    public async Task SendConfirmEmailAsync(string email,Guid userId)
    {
        string subject = "Confirm your email";
        
        string token= _tokenProvider.GenerateToken(userId);
        
        var confirmationLink = $"{_baseUrl}/confirm_email?emailToken={token}";

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
