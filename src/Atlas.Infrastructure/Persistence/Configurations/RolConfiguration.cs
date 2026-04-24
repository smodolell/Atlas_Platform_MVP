using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        // Configurar la tabla
        builder.ToTable("Roles");

        // Configurar propiedades
        builder.Property(r => r.Descripcion)
            .HasMaxLength(500)
            .HasColumnName("Descripcion")
            .HasColumnType("nvarchar(500)")
            .IsRequired(false); // nullable porque string? es opcional

        builder.Property(r => r.IsEnabled)
            .IsRequired()
            .HasColumnName("IsEnabled")
            .HasDefaultValue(true); // Valor por defecto true

        // Configurar propiedades heredadas de IdentityRole<int>
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("Name");

        builder.Property(r => r.NormalizedName)
            .HasMaxLength(100)
            .HasColumnName("NormalizedName");

        // Índices
        builder.HasIndex(r => r.Name)
            .IsUnique()
            .HasDatabaseName("IX_Roles_Name");

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .HasDatabaseName("IX_Roles_NormalizedName");

        //// Datos semilla (seed data) para roles predeterminados
        //builder.HasData(
        //    new Rol("Admin")
        //    {
        //        Id = 1,
        //        NormalizedName = "ADMIN",
        //        Descripcion = "Administrador del sistema",
        //        IsEnabled = true
        //    },
        //    new Rol("User")
        //    {
        //        Id = 2,
        //        NormalizedName = "USER",
        //        Descripcion = "Usuario estándar",
        //        IsEnabled = true
        //    },
        //    new Rol("Manager")
        //    {
        //        Id = 3,
        //        NormalizedName = "MANAGER",
        //        Descripcion = "Gerente con permisos especiales",
        //        IsEnabled = true
        //    }
        //);

    }
}