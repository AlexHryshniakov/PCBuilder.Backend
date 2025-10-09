using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface ITokenRepository
{
    Task SetRefreshToken(Guid userId, string token,DateTimeOffset expiresAt, CancellationToken cancellationToken);
    Task UpdateRefreshToken(Guid userId, string token, CancellationToken cancellationToken);
    Task RevocateRefreshToken(Guid userId, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshToken(string token, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshToken(Guid userId, CancellationToken cancellationToken);
}