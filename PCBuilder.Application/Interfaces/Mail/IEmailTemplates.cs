using PCBuidler.Domain.Enums;

namespace PCBuilder.Application.Interfaces.Mail;

public interface IEmailTemplates
{
    string GetTemplate(EmailTemplateTypes  type);
}