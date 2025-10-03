namespace PCBuilder.Application.Interfaces.Mail;

public interface IEmailService
{
     Task SendConfirmEmailAsync(string email,string token);
     Task SendResetPasswordEmailAsync(string email, string token);
}