using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCBuilder.Persistence.Entities.Components;

namespace PCBuilder.Persistence.Configurations.Components;

public class CpuConfiguration:IEntityTypeConfiguration<CpuEntity> 
{
    public void Configure(EntityTypeBuilder<CpuEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(c => c.Socket) 
            .WithMany(s => s.Cpus) 
            .HasForeignKey(c => c.SocketId) 
            .IsRequired() 
            .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Manufacturer).IsRequired().HasMaxLength(50);
        
        builder.HasIndex(c => c.Manufacturer);
        builder.HasIndex(c => c.Name).IsUnique();
        builder.HasIndex(c => c.SocketId);

        builder.Property(c => c.BaseClockSpeed).IsRequired();
        builder.Property(c => c.Tdp).IsRequired();
        builder.Property(c => c.MaxMemorySpeed).IsRequired();
        
        builder.Property(c => c.BoostClockSpeed).IsRequired(false); 

        builder.Property(c => c.PerformanceCores).IsRequired(false);
        builder.Property(c => c.EfficiencyCores).IsRequired(false);
    }
}