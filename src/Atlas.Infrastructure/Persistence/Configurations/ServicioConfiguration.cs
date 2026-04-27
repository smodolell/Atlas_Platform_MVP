using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Atlas.Domain.Entities;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        // Nombre de la tabla
        builder.ToTable("Servicios");

        // Llave primaria
        builder.HasKey(s => s.Id);

        // Configurar el Id como auto-incremental
        builder.Property(s => s.Id)
            .UseIdentityColumn();

        // Configurar NomServicio
        builder.Property(s => s.NomServicio)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnOrder(2);

        // Configurar Descripcion
        builder.Property(s => s.Descripcion)
            .IsRequired(false) // Puede ser nulo
            .HasMaxLength(500)
            .HasColumnOrder(3);

        // Configurar Activo
        builder.Property(s => s.Activo)
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnOrder(4);

        // Configurar la relación con Plan (uno a muchos)
        builder.HasMany(s => s.Planes)
            .WithOne(p => p.Servicio) // Asumiendo que Plan tiene una propiedad Servicio
            .HasForeignKey(p => p.ServicioId) // Asumiendo que Plan tiene ServicioId
            .OnDelete(DeleteBehavior.Restrict); // O Cascade, SetNull, etc.
        builder.HasMany(s => s.Horarios)
          .WithOne(sh => sh.Servicio) // Asumiendo que ServicioHorario tiene una propiedad Servicio
          .HasForeignKey(sh => sh.ServicioId) // Asumiendo que ServicioHorario tiene ServicioId
          .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(s => s.NomServicio)
            .IsUnique()
            .HasDatabaseName("IX_Servicios_NombreServicio");

        builder.HasIndex(s => s.Activo)
            .HasDatabaseName("IX_Servicios_Activo");
    }
}
