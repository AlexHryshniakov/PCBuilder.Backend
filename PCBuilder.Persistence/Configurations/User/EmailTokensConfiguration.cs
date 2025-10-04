using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCBuilder.Persistence.Entities;
using PCBuilder.Persistence.Entities.User;

namespace PCBuilder.Persistence.Configurations.User;

public class EmailTokensConfiguration:IEntityTypeConfiguration<EmailTokensEntity>
{
    public void Configure(EntityTypeBuilder<EmailTokensEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e=>e.Id).IsUnique(); 
    }
}