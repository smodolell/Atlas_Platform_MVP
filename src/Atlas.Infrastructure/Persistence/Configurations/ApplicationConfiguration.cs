using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Entities.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
    {
        builder.ToTable("Applications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.ApplicationName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.HasIndex(e => e.ApplicationName)
            .IsUnique()
            .HasDatabaseName("IX_Applications_ApplicationName");
    }
}
