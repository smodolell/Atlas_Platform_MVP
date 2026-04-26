using Atlas.Domain.Entities;
using Atlas.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class PeriodicidadConfiguration : IEntityTypeConfiguration<Periodicidad>
{
    public void Configure(EntityTypeBuilder<Periodicidad> builder)
    {
        // Configurar la tabla
        builder.ToTable("Periodicidades");

        // Configurar clave primaria
        builder.HasKey(p => p.Id);

        // Configurar propiedades
        builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1);

        builder.Property(p => p.NomPeriodicidad)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("NomPeriodicidad")
            .HasColumnType("nvarchar(50)");

        builder.Property(p => p.Unidad)
           .IsRequired()
           .HasColumnName("Unidad");

        builder.Property(p => p.Valor)
            .IsRequired()
            .HasColumnName("Valor");

        builder.Property(p => p.Activa)
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnName("Activa");

        // Índices
        builder.HasIndex(p => p.NomPeriodicidad)
            .IsUnique()
            .HasDatabaseName("IX_Periodicidades_NomPeriodicidad");

        builder.HasIndex(p => p.Activa)
            .HasDatabaseName("IX_Periodicidades_Activa");

        // Relación con Producto (uno a muchos)
        builder.HasMany(p => p.Planes)
            .WithOne(prod => prod.Periodicidad)
            .HasForeignKey(prod => prod.PeriodicidadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
           new Periodicidad { Id = 1, NomPeriodicidad = "Diaria", Unidad = UnidadTiempo.Dias, Valor = 1, Activa = true },
           new Periodicidad { Id = 2, NomPeriodicidad = "Semanal", Unidad = UnidadTiempo.Dias, Valor = 7, Activa = true },
           new Periodicidad { Id = 3, NomPeriodicidad = "Quincenal", Unidad = UnidadTiempo.Dias, Valor = 15, Activa = true },
           new Periodicidad { Id = 4, NomPeriodicidad = "Mensual", Unidad = UnidadTiempo.Meses, Valor = 1, Activa = true }
       );
    }
}