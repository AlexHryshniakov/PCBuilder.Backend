using PCBuilder.Core.Enums;
using PCBuilder.Application.Interfaces.Mail;

namespace PCBuilder.Infrastructure.EmailMessage;

public class EmailTemplates(Dictionary<EmailTemplateTypes, string> templates)
    : IEmailTemplates
{
    public string GetTemplate(EmailTemplateTypes  type) => templates[type];
}