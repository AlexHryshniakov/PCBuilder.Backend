using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Configurations;

public class UserRoleConfiguration:IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
    }
}