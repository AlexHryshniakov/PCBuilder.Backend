namespace PCBuilder.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = String.Empty;
    public string Email { get; init; } = String.Empty;
    public string PasswordHash { get; init; } = String.Empty;
    
    public bool EmailConfirmed { get; init; }
    public string AvatarUrl { get; init; } = String.Empty;
    public ICollection<RoleEntity> Roles { get; init; } = [];
}