using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Configurations;

public class RefreshTokenConfiguration:IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(r => r.Token);
        builder.HasIndex(r => r.UserId);
    }
}