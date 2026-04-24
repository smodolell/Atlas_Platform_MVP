using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class RolAccessPointConfiguration : IEntityTypeConfiguration<RolAccessPoint>
{
    public void Configure(EntityTypeBuilder<RolAccessPoint> builder)
    {
        builder.ToTable("RolAccessPoints");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Rol)
            .WithMany()
            .HasForeignKey(e => e.RolId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.AccessPoint)
            .WithMany(a => a.SYS_RolAccessPoint)
            .HasForeignKey(e => e.AccessPointId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.RolId, e.AccessPointId })
            .IsUnique()
            .HasDatabaseName("IX_RolAccessPoints_RolId_AccessPointId");

        builder.HasIndex(e => e.AccessPointId)
            .HasDatabaseName("IX_RolAccessPoints_AccessPointId");
    }
}
