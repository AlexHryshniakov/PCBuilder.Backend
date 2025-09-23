using PCBuidler.Domain.Enums;
using PCBuidler.Domain.Models;

namespace PCBuilder.Application.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(User user,CancellationToken cancellationToken);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    Task<HashSet<Permission>> GetUserPermissions(Guid userId, CancellationToken cancellationToken);
    Task ConfirmEmail(Guid id,  CancellationToken cancellationToken);
}