using PCBuilder.Core.Enums;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services;

public class PermissionService(IUsersRepository usersRepository) : IPermissionService
{
    private readonly IUsersRepository _usersRepository=usersRepository;

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId, CancellationToken ct)
    {
        return _usersRepository.GetUserPermissions(userId,ct);
    }
}