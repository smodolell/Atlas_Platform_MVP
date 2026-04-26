using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class MembresiaConfiguration : IEntityTypeConfiguration<Membresia>
{
    public void Configure(EntityTypeBuilder<Membresia> builder)
    {
        // Configurar la tabla
        builder.ToTable("Membresias");

        // Configurar clave primaria
        builder.HasKey(m => m.Id);

        // Configurar propiedades
        builder.Property(m => m.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(m => m.SocioId)
            .IsRequired();

        builder.Property(m => m.PlanId)
            .IsRequired();

        // Configurar propiedades de montos con precisión decimal
        builder.Property(m => m.Monto)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Property(m => m.IVA)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Property(m => m.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        // Configurar propiedades de saldos
        builder.Property(m => m.MontoSaldo)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(m => m.IVASaldo)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.Property(m => m.TotalSaldo)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        // Configurar fechas
        builder.Property(m => m.FechaInicio)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(m => m.FechaVencimiento)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(m => m.FechaFinalizacion)
            .HasColumnType("datetime2");

        builder.Property(m => m.DiasGracia)
            .IsRequired()
            .HasDefaultValue(0);

        // Configurar relaciones
        builder.HasOne(m => m.Socio)
            .WithMany()
            .HasForeignKey(m => m.SocioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Plan)
            .WithMany()
            .HasForeignKey(m => m.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(m => m.SocioId)
            .HasDatabaseName("IX_Membresias_SocioId");

        builder.HasIndex(m => m.PlanId)
            .HasDatabaseName("IX_Membresias_ProductoId");

        builder.HasIndex(m => m.FechaVencimiento)
            .HasDatabaseName("IX_Membresias_FechaVencimiento");

        builder.HasIndex(m => new { m.SocioId, m.FechaVencimiento })
            .HasDatabaseName("IX_Membresias_Socio_FechaVencimiento");
    }
}