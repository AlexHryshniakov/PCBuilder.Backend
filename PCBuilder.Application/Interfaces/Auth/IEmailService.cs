namespace PCBuilder.Application.Interfaces.Auth;

public interface IEmailService
{
     Task SendConfirmEmailAsync(string email, Guid userId);
}