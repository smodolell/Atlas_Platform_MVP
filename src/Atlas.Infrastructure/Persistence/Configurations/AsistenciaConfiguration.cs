using Atlas.Domain.Entities;
using Atlas.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
{
    public void Configure(EntityTypeBuilder<Asistencia> builder)
    {
        // Tabla y esquema
        builder.ToTable("Asistencias");

        // Clave primaria
        builder.HasKey(a => a.Id);

        // Propiedades
        builder.Property(a => a.Id)
            .HasDefaultValueSql("NEWID()");  

        builder.Property(a => a.SocioId)
            .IsRequired();

        builder.Property(a => a.Estatus)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),           // Enum a string
                v => (EstatusAsistencia)Enum.Parse(typeof(EstatusAsistencia), v)
            )
            .HasMaxLength(20);

        builder.Property(a => a.FechaHoraEntrada)
            .IsRequired()
            .HasColumnType("datetime2(7)");

        builder.Property(a => a.FechaHoraSalida)
            .HasColumnType("datetime2(7)");

        // Índices para búsquedas frecuentes
        builder.HasIndex(a => a.SocioId)
            .HasDatabaseName("IX_Asistencias_SocioId");

        builder.HasIndex(a => a.FechaHoraEntrada)
            .HasDatabaseName("IX_Asistencias_FechaHoraEntrada");

        // Índice compuesto: socio + fechas (útil para reportes)
        builder.HasIndex(a => new { a.SocioId, a.FechaHoraEntrada })
            .HasDatabaseName("IX_Asistencias_Socio_FechaEntrada");

        // Relaciones
        builder.HasOne(a => a.Socio)
            .WithMany(s => s.Asistencias)  // Asumiendo que Socio tiene ICollection<Asistencia>
            .HasForeignKey(a => a.SocioId)
            .OnDelete(DeleteBehavior.Restrict);  // No borrar socio si tiene asistencias

  
        builder.HasOne(a => a.Plan)
            .WithMany(p => p.Asistencias)
            .HasForeignKey(a => a.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Sesion)
            .WithMany(ps => ps.Asistencias)
            .HasForeignKey(a => a.PlanSesionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}