using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class AccessPointTypeConfiguration : IEntityTypeConfiguration<AccessPointType>
{
    public void Configure(EntityTypeBuilder<AccessPointType> builder)
    {
        builder.ToTable("AccessPointType");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.AccessPointTypeName)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasIndex(e => e.AccessPointTypeName)
            .IsUnique()
            .HasDatabaseName("IX_SYS_AccessPointTypes_AccessPointTypeName");


        builder.HasData(
           new AccessPointType { Id = 0, AccessPointTypeName = "LeftMenu" },
           new AccessPointType { Id = 1, AccessPointTypeName = "Page" },
           new AccessPointType { Id = 2, AccessPointTypeName = "Element" }
       );
    }
}
