using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        // Configurar la tabla
        builder.ToTable("Productos");

        // Configurar clave primaria
        builder.HasKey(p => p.Id);

        // Configurar propiedades
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1);

        builder.Property(p => p.PeriodicidadId)
            .IsRequired();

        builder.Property(p => p.NomProducto)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("NomProducto")
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
            .WithMany(p => p.Productos)
            .HasForeignKey(p => p.PeriodicidadId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relación con Membresia (si existe)
        builder.HasMany(p => p.Membresias)
            .WithOne(m => m.Producto)
            .HasForeignKey(m => m.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(p => p.NomProducto)
            .IsUnique()
            .HasDatabaseName("IX_Productos_NomProducto");

        builder.HasIndex(p => p.PeriodicidadId)
            .HasDatabaseName("IX_Productos_PeriodicidadId");

        builder.HasIndex(p => p.Activo)
            .HasDatabaseName("IX_Productos_Activo");

        builder.HasIndex(p => new { p.Activo, p.NomProducto })
            .HasDatabaseName("IX_Productos_Activo_Nombre");
    }
}