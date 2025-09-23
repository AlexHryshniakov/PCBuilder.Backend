namespace PCBuilder.Application.Interfaces.Mail;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject,string body);
}