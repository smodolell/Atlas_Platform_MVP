using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class PagoConfiguration : IEntityTypeConfiguration<Pago>
{
    public void Configure(EntityTypeBuilder<Pago> builder)
    {
        // Configurar la tabla
        builder.ToTable("Pagos");

        // Configurar clave primaria
        builder.HasKey(p => p.Id);

        // Configurar propiedades
        builder.Property(p => p.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(p => p.TipoPagoId)
            .IsRequired();

        builder.Property(p => p.FechaPago)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.MontoPago)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        // Configurar relación con TipoPago
        builder.HasOne(p => p.TipoPago)
            .WithMany()
            .HasForeignKey(p => p.TipoPagoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relación con MembresiaPago
        builder.HasMany(p => p.MembresiaPagos)
            .WithOne(mp => mp.Pago)
            .HasForeignKey(mp => mp.PagoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(p => p.TipoPagoId)
            .HasDatabaseName("IX_Pagos_TipoPagoId");

        builder.HasIndex(p => p.FechaPago)
            .HasDatabaseName("IX_Pagos_FechaPago");

        builder.HasIndex(p => new { p.FechaPago, p.TipoPagoId })
            .HasDatabaseName("IX_Pagos_Fecha_TipoPago");
    }
}