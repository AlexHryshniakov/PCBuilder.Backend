using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface IEmailRepositories
{
    public Task<EmailTokens> GetEmailTokens(Guid userId, CancellationToken ct);
    
    public Task AddEmailTokens(Guid userId, string confirmEmailToken, int confirmEmailExpiresAt, CancellationToken cancellationToken);
    public  Task ApplyConfirmEmailTokens(string confirmToken, CancellationToken cancellationToken);
    public Task<EmailTokens> GetEmailTokensByConfirmToken(string confirmToken, CancellationToken cancellationToken);
    
    public Task ResetPassword(Guid userId, string passwordResetToken, int passwordResetExpiresAt, CancellationToken cancellationToken);
    public Task AllowedChangePassword(string passwordResetToken, CancellationToken cancellationToken);
    public Task ApplyResetPassword(Guid userId, CancellationToken cancellationToken);

    Task<EmailTokens> GetEmailTokensByResetPasswordToken(string resetPasswordToken, CancellationToken cancellationToken);


}