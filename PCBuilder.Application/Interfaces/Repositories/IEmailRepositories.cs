using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface IEmailRepositories
{
    public Task<EmailTokens> GetEmailTokens(Guid userId, CancellationToken ct);
    
    public Task AddEmailTokens(Guid userId, string confirmEmailToken, TimeSpan confirmEmailExpiresAt, CancellationToken cancellationToken);
    public  Task ApplyConfirmEmailTokens(string confirmToken, CancellationToken cancellationToken);

    public Task ResetPassword(Guid userId, string passwordResetToken, DateTimeOffset passwordResetExpiresAt, CancellationToken cancellationToken);
    public Task AllowedChangePassword(Guid userId, CancellationToken cancellationToken);
    public Task ApplyResetPassword(string resetPasswordToken, CancellationToken cancellationToken);




}