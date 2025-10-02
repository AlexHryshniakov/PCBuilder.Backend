using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PCBuilder.Persistence.Configurations;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence;

public class PcBuilderDbContext(
    DbContextOptions<PcBuilderDbContext> options,
    IOptions<AuthorizationOptions> authOptions):DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    public DbSet<EmailTokensEntity> EmailTokens { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //   modelBuilder.ApplyConfigurationsFromAssembly(typeof(PcBuilderDbContext).Assembly);

        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTokensConfiguration());
        
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
    }
}