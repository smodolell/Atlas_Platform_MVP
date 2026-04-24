using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class SocioConfiguration : IEntityTypeConfiguration<Socio>
{
    public void Configure(EntityTypeBuilder<Socio> builder)
    {
        // Configurar la tabla
        builder.ToTable("Socios");

        // Configurar clave primaria
        builder.HasKey(s => s.Id);

        // Configurar propiedades
        builder.Property(s => s.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()"); // Generar GUID automáticamente

        builder.Property(s => s.FechaRegistro)
            .IsRequired()
            .HasColumnName("FechaRegistro")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()"); // Fecha actual por defecto

        builder.Property(s => s.Nombre)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("Nombre")
            .HasColumnType("nvarchar(100)");

        builder.Property(s => s.Apellido)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("Apellido")
            .HasColumnType("nvarchar(100)");

        builder.Property(s => s.DNI)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("DNI")
            .HasColumnType("nvarchar(20)");

        builder.Property(s => s.FechaNacimiento)
            .IsRequired()
            .HasColumnName("FechaNacimiento")
            .HasColumnType("date"); // Solo fecha, sin hora

        builder.Property(s => s.Email)
            .HasMaxLength(200)
            .HasColumnName("Email")
            .HasColumnType("nvarchar(200)")
            .IsRequired(false);

        builder.Property(s => s.Telefono)
            .HasMaxLength(20)
            .HasColumnName("Telefono")
            .HasColumnType("nvarchar(20)")
            .IsRequired(false);

        // Índices
        builder.HasIndex(s => s.DNI)
            .IsUnique()
            .HasDatabaseName("IX_Socios_DNI");

        builder.HasIndex(s => s.Email)
            .IsUnique()
            .HasDatabaseName("IX_Socios_Email")
            .HasFilter("[Email] IS NOT NULL"); // Solo para emails no nulos

        builder.HasIndex(s => new { s.Apellido, s.Nombre })
            .HasDatabaseName("IX_Socios_Apellido_Nombre");

        // Configuración de concurrencia (opcional)
        builder.Property(s => s.FechaRegistro)
            .IsConcurrencyToken();
    }
}
