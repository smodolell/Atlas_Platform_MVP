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
