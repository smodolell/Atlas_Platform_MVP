using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Configurations;

public class ServicioHorarioConfiguration : IEntityTypeConfiguration<ServicioHorario>
{
    public void Configure(EntityTypeBuilder<ServicioHorario> builder)
    {
        // Nombre de la tabla
        builder.ToTable("ServicioHorarios");

        // Llave primaria
        builder.HasKey(sh => sh.Id);

        // Configurar Id como auto-incremental
        builder.Property(sh => sh.Id)
            .UseIdentityColumn();

        // Configurar ServicioId (clave foránea)
        builder.Property(sh => sh.ServicioId)
            .IsRequired();

        // Configurar EmpleadoId (clave foránea)
        builder.Property(sh => sh.EmpleadoId)
            .IsRequired();

        // Configurar DiaSemana
        builder.Property(sh => sh.DiaSemana)
            .IsRequired()
            .HasConversion<int>() // Convierte el enum a int para la BD
            .HasColumnName("DiaSemana");

        // Configurar HoraInicio (TimeOnly)
        builder.Property(sh => sh.HoraInicio)
            .IsRequired()
            .HasConversion(
                time => time.ToTimeSpan(),      // TimeOnly -> TimeSpan para BD
                timeSpan => TimeOnly.FromTimeSpan(timeSpan) // TimeSpan -> TimeOnly
            )
            .HasColumnName("HoraInicio");

        // Configurar HoraFin (TimeOnly)
        builder.Property(sh => sh.HoraFin)
            .IsRequired()
            .HasConversion(
                time => time.ToTimeSpan(),
                timeSpan => TimeOnly.FromTimeSpan(timeSpan)
            )
            .HasColumnName("HoraFin");

        // Configurar Activo
        builder.Property(sh => sh.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Relación con Servicio (muchos a uno)
        builder.HasOne(sh => sh.Servicio)
            .WithMany(s => s.Horarios) // Asumiendo que Servicio tiene ICollection<ServicioHorario>
            .HasForeignKey(sh => sh.ServicioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación con Empleado (muchos a uno)
        builder.HasOne(sh => sh.Empleado)
            .WithMany(e => e.ServicioHorarios) // Asumiendo que Empleado tiene ICollection<ServicioHorario>
            .HasForeignKey(sh => sh.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices para mejorar el rendimiento
        builder.HasIndex(sh => new { sh.ServicioId, sh.EmpleadoId, sh.DiaSemana })
            .HasDatabaseName("IX_ServiciosHorarios_Servicio_Empleado_Dia");

        builder.HasIndex(sh => sh.EmpleadoId)
            .HasDatabaseName("IX_ServiciosHorarios_EmpleadoId");

        builder.HasIndex(sh => sh.ServicioId)
            .HasDatabaseName("IX_ServiciosHorarios_ServicioId");

        builder.HasIndex(sh => sh.Activo)
            .HasDatabaseName("IX_ServiciosHorarios_Activo");

        // Restricción única: Un empleado no puede tener dos horarios en el mismo día y hora
        builder.HasIndex(sh => new { sh.EmpleadoId, sh.DiaSemana, sh.HoraInicio })
            .IsUnique()
            .HasDatabaseName("UK_ServiciosHorarios_Empleado_Dia_HoraInicio");

        // Validación adicional (opcional - configurable en BD)
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_ServiciosHorarios_HoraInicio_Menor_HoraFin",
            "HoraInicio < HoraFin"));
    }
}