using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleados");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .UseIdentityColumn();

        builder.Property(e => e.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Apellido)
            .IsRequired()
            .HasMaxLength(100);

        // Índice para búsqueda por nombre
        builder.HasIndex(e => new { e.Nombre, e.Apellido })
            .HasDatabaseName("IX_Empleado_NombreApellido");
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