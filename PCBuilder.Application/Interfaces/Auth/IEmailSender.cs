namespace PCBuilder.Application.Interfaces.Auth;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject,string body);
}