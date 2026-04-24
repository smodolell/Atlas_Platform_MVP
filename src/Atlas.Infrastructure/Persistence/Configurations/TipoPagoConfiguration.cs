using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class TipoPagoConfiguration : IEntityTypeConfiguration<TipoPago>
{
    public void Configure(EntityTypeBuilder<TipoPago> builder)
    {
        // Configurar la tabla
        builder.ToTable("TiposPago");

        // Configurar clave primaria
        builder.HasKey(tp => tp.Id);

        // Configurar propiedades
        builder.Property(tp => tp.Id)
            .IsRequired()
            .ValueGeneratedOnAdd(); // Auto-increment

        builder.Property(tp => tp.NomTipoPago)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("NomTipoPago")
            .HasColumnType("nvarchar(100)");

        builder.Property(tp => tp.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Índices
        builder.HasIndex(tp => tp.NomTipoPago)
            .IsUnique()
            .HasDatabaseName("IX_TiposPago_NomTipoPago");

        // Relación con Pago (uno a muchos)
        builder.HasMany(tp => tp.Pagos)
            .WithOne(p => p.TipoPago)
            .HasForeignKey(p => p.TipoPagoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}