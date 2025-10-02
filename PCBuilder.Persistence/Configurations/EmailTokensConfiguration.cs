using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCBuilder.Persistence.Entities;

namespace PCBuilder.Persistence.Configurations;

public class EmailTokensConfiguration:IEntityTypeConfiguration<EmailTokensEntity>
{
    public void Configure(EntityTypeBuilder<EmailTokensEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e=>e.Id).IsUnique();
        builder.HasIndex(e=>e.PasswordResetToken).IsUnique();
        builder.HasIndex(e=>e.ConfirmEmailToken).IsUnique();
    }
}