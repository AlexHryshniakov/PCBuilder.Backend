using AutoMapper;
using PCBuidler.Domain.Models;
using PCBuilder.Application.Common.Mapping;

namespace PCBuilder.Persistence.Entities;

public class UserEntity 
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = String.Empty;
    public string Email { get; init; } = String.Empty;
    public string PasswordHash { get; init; } = String.Empty;
    
    public bool EmailConfirmed { get; set; }
    public string AvatarUrl { get; set; } = String.Empty;
    public ICollection<RoleEntity> Roles { get; init; } = [];

   
}