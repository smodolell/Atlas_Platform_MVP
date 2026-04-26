using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        // Configurar la tabla
        builder.ToTable("Planes");

        // Configurar clave primaria
        builder.HasKey(p => p.Id);

        // Configurar propiedades
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1);

        builder.Property(p => p.PeriodicidadId)
            .IsRequired();

        builder.Property(p => p.NomPlan)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("NomPlan")
            .HasColumnType("nvarchar(200)");

        builder.Property(p => p.Descripcion)
            .HasMaxLength(1000)
            .HasColumnName("Descripcion")
            .HasColumnType("nvarchar(1000)");

        builder.Property(p => p.Precio)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Property(p => p.CupoMaximo)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Configurar relación con Periodicidad
        builder.HasOne(p => p.Periodicidad)
            .WithMany(p => p.Planes)
            .HasForeignKey(p => p.PeriodicidadId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relación con Membresia (si existe)
        builder.HasMany(p => p.Membresias)
            .WithOne(m => m.Plan)
            .HasForeignKey(m => m.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(p => p.NomPlan)
            .IsUnique()
            .HasDatabaseName("IX_Planes_NomPlan");

        builder.HasIndex(p => p.PeriodicidadId)
            .HasDatabaseName("IX_Planes_PeriodicidadId");

        builder.HasIndex(p => p.Activo)
            .HasDatabaseName("IX_Planes_Activo");

        builder.HasIndex(p => new { p.Activo, p.NomPlan })
            .HasDatabaseName("IX_Planes_Activo_NomPlan");
    }
}