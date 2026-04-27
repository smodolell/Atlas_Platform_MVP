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
