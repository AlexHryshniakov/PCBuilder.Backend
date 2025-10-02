namespace PCBuilder.Application.Interfaces.Mail;

public interface IEmailService
{
     Task SendConfirmEmailAsync(string email,string token, Guid userId,CancellationToken cancellationToken);
}