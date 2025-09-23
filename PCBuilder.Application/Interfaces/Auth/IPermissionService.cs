using PCBuidler.Domain.Enums;

namespace PCBuilder.Application.Interfaces.Auth;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId,CancellationToken cancellationToken);
}
