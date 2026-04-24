using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.Property(e => e.Icon)
            .HasMaxLength(1000)
            .HasColumnType("varchar(1000)");

        builder.Property(e => e.Order)
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Menus_Name");
    }
}
