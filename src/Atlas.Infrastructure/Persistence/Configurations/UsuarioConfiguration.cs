using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        // Configurar la tabla (opcional, por defecto usa el nombre de la clase)
        builder.ToTable("Usuarios");

        // Configurar la clave primaria (IdentityUser<int> ya la tiene configurada)
        // Pero puedes personalizarla si es necesario
        builder.HasKey(u => u.Id);

        // Configurar propiedades
        builder.Property(u => u.NombreCompleto)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("NombreCompleto")
            .HasColumnType("nvarchar(200)");

        builder.Property(u => u.Telefono)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("Telefono")
            .HasColumnType("nvarchar(50)");

        builder.Property(u => u.FechaRegistro)
            .IsRequired()
            .HasColumnName("FechaRegistro")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETDATE()"); // Valor por defecto

        builder.Property(u => u.Avatar)
            .HasMaxLength(200)
            .HasColumnName("Avatar")
            .HasColumnType("nvarchar(200)")
            .HasDefaultValue(""); // Valor por defecto vacío

        // Índices
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.Telefono)
            .HasDatabaseName("IX_Usuario_Telefono");

    }
}

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
        builder.HasMany(p => p.Productos)
            .WithOne(prod => prod.Periodicidad)
            .HasForeignKey(prod => prod.PeriodicidadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}