using Atlas.Domain.Entities;
using Atlas.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class PlanSesionConfiguration : IEntityTypeConfiguration<PlanSesion>
{
    public void Configure(EntityTypeBuilder<PlanSesion> builder)
    {
        builder.ToTable("PlanesSesion");

        builder.HasKey(ps => ps.Id);

        builder.Property(ps => ps.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(ps => ps.Fecha)
            .IsRequired();

        builder.Property(ps => ps.HoraInicio)
            .IsRequired();

        builder.Property(ps => ps.HoraFin)
            .IsRequired();

        builder.Property(ps => ps.Estado)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(EstadoSesion.Programada);

        // Índices
        builder.HasIndex(ps => new { ps.PlanId, ps.Fecha })
            .HasDatabaseName("IX_PlanSesion_PlanFecha");

        builder.HasIndex(ps => ps.PlanHorarioId)
            .HasDatabaseName("IX_PlanSesion_PlanHorarioId");

        builder.HasIndex(ps => ps.EmpleadoId)
            .HasDatabaseName("IX_PlanSesion_EmpleadoId");

        builder.HasIndex(ps => ps.Estado)
            .HasDatabaseName("IX_PlanSesion_Estado");

        // Índice compuesto para búsquedas comunes
        builder.HasIndex(ps => new { ps.Fecha, ps.Estado, ps.PlanId })
            .HasDatabaseName("IX_PlanSesion_FechaEstadoPlan");

        // Relaciones
        builder.HasOne(ps => ps.Plan)
            .WithMany(p => p.Sesiones)
            .HasForeignKey(ps => ps.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ps => ps.Horario)
            .WithMany()
            .HasForeignKey(ps => ps.PlanHorarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ps => ps.Empleado)
            .WithMany()
            .HasForeignKey(ps => ps.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

//public class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
//{
//    public void Configure(EntityTypeBuilder<Asistencia> builder)
//    {
//        builder.ToTable("Asistencias");

//        builder.HasKey(a => a.Id);

//        builder.Property(a => a.Id)
//            .HasDefaultValueSql("NEWID()");

//        builder.Property(a => a.Estatus)
//            .IsRequired()
//            .HasConversion<int>()
//            .HasDefaultValue(EstatusAsistencia.Presente);

//        builder.Property(a => a.FechaHoraEntrada)
//            .IsRequired()
//            .HasColumnType("datetime2");

//        builder.Property(a => a.FechaHoraSalida)
//            .HasColumnType("datetime2");

//        // Índices
//        builder.HasIndex(a => a.SocioId)
//            .HasDatabaseName("IX_Asistencia_SocioId");

//        builder.HasIndex(a => a.PlanSesionId)
//            .HasDatabaseName("IX_Asistencia_PlanSesionId");

//        builder.HasIndex(a => a.PlanId)
//            .HasDatabaseName("IX_Asistencia_PlanId");

//        // Índice compuesto para reportes de asistencia
//        builder.HasIndex(a => new { a.PlanSesionId, a.Estatus })
//            .HasDatabaseName("IX_Asistencia_SesionEstatus");

//        builder.HasIndex(a => new { a.SocioId, a.FechaHoraEntrada })
//            .HasDatabaseName("IX_Asistencia_SocioFecha");

//        // Relaciones
//        builder.HasOne(a => a.Socio)
//            .WithMany(s => s.Asistencias)
//            .HasForeignKey(a => a.SocioId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder.HasOne(a => a.Plan)
//            .WithMany(p => p.Asistencias)
//            .HasForeignKey(a => a.PlanId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder.HasOne(a => a.Sesion)
//            .WithMany(ps => ps.Asistencias)
//            .HasForeignKey(a => a.PlanSesionId)
//            .OnDelete(DeleteBehavior.Restrict);

//        // Validación: FechaHoraSalida debe ser mayor que FechaHoraEntrada
//        builder.ToTable(tb => tb.HasCheckConstraint(
//            "CK_Asistencia_FechaSalida",
//            "FechaHoraSalida IS NULL OR FechaHoraSalida > FechaHoraEntrada"));
//    }
//}