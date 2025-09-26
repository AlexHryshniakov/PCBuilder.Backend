using PCBuidler.Domain.Enums;
using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(User user,CancellationToken cancellationToken);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    Task<HashSet<Permission>> GetUserPermissions(Guid userId, CancellationToken cancellationToken);
    Task<User> GetById(Guid userId, CancellationToken cancellationToken);
    Task ConfirmEmail(Guid id,  CancellationToken cancellationToken);
    

    Task SetRefreshToken(Guid userId, string token,DateTimeOffset expiresAt, CancellationToken cancellationToken);
    Task UpdateRefreshToken(Guid userId, string token, CancellationToken cancellationToken);
    Task RevocateRefreshToken(Guid userId, CancellationToken cancellationToken);
    Task<RefreshToken?> GetUserIdByRt(string token, CancellationToken cancellationToken);
}