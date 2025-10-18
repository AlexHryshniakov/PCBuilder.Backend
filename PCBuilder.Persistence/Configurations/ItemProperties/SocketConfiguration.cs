using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCBuilder.Persistence.Entities.ItemProperties;

namespace PCBuilder.Persistence.Configurations.ItemProperties;

public class SocketConfiguration:IEntityTypeConfiguration<SocketEntity>
{
    public void Configure(EntityTypeBuilder<SocketEntity> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50); 
            
        builder.HasIndex(s => s.Name)
            .IsUnique();
    }
}