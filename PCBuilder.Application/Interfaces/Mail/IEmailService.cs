namespace PCBuilder.Application.Interfaces.Mail;

public interface IEmailService
{
     Task SendConfirmEmailAsync(string email, Guid userId);
}