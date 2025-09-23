using Microsoft.AspNetCore.Authorization;
using PCBuidler.Domain.Enums;

namespace PCBuilder.Infrastructure.Authentication;

public class PermissionRequirement(Permission[] permissions): IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}