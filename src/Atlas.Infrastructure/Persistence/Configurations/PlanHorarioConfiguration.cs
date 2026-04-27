using Atlas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Infrastructure.Persistence.Configurations;

public class PlanHorarioConfiguration : IEntityTypeConfiguration<PlanHorario>
{
    public void Configure(EntityTypeBuilder<PlanHorario> builder)
    {
        builder.ToTable("PlanesHorario");

        builder.HasKey(ph => ph.Id);

        builder.Property(ph => ph.Id)
            .UseIdentityColumn();

        builder.Property(ph => ph.DiaSemana)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(ph => ph.HoraInicio)
            .IsRequired();

        builder.Property(ph => ph.HoraFin)
            .IsRequired();

        builder.Property(ph => ph.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Índices para optimizar búsquedas
        builder.HasIndex(ph => new { ph.PlanId, ph.EmpleadoId, ph.DiaSemana })
            .HasDatabaseName("IX_PlanHorario_PlanEmpleadoDia");

        builder.HasIndex(ph => ph.EmpleadoId)
            .HasDatabaseName("IX_PlanHorario_EmpleadoId");

        // Relaciones
        builder.HasOne(ph => ph.Plan)
            .WithMany(p => p.Horarios)
            .HasForeignKey(ph => ph.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ph => ph.Empleado)
            .WithMany(e => e.PlanHorarios)
            .HasForeignKey(ph => ph.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Validaciones
        builder.ToTable(tb => tb.HasCheckConstraint(
    "CK_PlanHorario_HoraFin",
    "HoraFin > HoraInicio"));
    }
}
