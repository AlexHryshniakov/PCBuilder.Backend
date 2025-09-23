using PCBuidler.Domain.Enums;
using PCBuilder.Application.Interfaces.Auth;
using PCBuilder.Application.Interfaces.Repositories;

namespace PCBuilder.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUsersRepository _usersRepository;

    public PermissionService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId, CancellationToken ct)
    {
        return _usersRepository.GetUserPermissions(userId,ct);
    }
}