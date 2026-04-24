using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class MembresiaPagoConfiguration : IEntityTypeConfiguration<MembresiaPago>
{
    public void Configure(EntityTypeBuilder<MembresiaPago> builder)
    {
        // Configurar la tabla
        builder.ToTable("MembresiasPagos");

        // Configurar clave primaria
        builder.HasKey(mp => mp.Id);

        // Configurar propiedades
        builder.Property(mp => mp.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(mp => mp.MembresiaId)
            .IsRequired();

        builder.Property(mp => mp.PagoId)
            .IsRequired();

        // Configurar montos con precisión decimal
        builder.Property(mp => mp.Monto)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Property(mp => mp.IVA)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Property(mp => mp.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        // Configurar fecha de pago
        builder.Property(mp => mp.FechaPago)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()");

        // Configurar relaciones
        builder.HasOne(mp => mp.Membresia)
            .WithMany(m => m.MembresiaPagos) // Asumiendo que Membresia tiene una colección
            .HasForeignKey(mp => mp.MembresiaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(mp => mp.Pago)
            .WithMany(p => p.MembresiaPagos) // Asumiendo que Pago tiene una colección
            .HasForeignKey(mp => mp.PagoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(mp => mp.MembresiaId)
            .HasDatabaseName("IX_MembresiasPagos_MembresiaId");

        builder.HasIndex(mp => mp.PagoId)
            .HasDatabaseName("IX_MembresiasPagos_PagoId");

        builder.HasIndex(mp => mp.FechaPago)
            .HasDatabaseName("IX_MembresiasPagos_FechaPago");

        // Índice compuesto único para evitar duplicados
        builder.HasIndex(mp => new { mp.MembresiaId, mp.PagoId })
            .IsUnique()
            .HasDatabaseName("UK_MembresiasPagos_MembresiaPago");
    }
}