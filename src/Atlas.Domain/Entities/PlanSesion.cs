using Atlas.Domain.Enums;

namespace Atlas.Domain.Entities;

public class PlanSesion
{
    public Guid Id { get; set; }

    public int PlanId { get; set; }
    public int PlanHorarioId { get; set; }

    public int EmpleadoId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }

    public EstadoSesion Estado { get; set; }

    public Plan Plan { get; set; } = null!;
    public PlanHorario Horario { get; set; } = null!;
    public Empleado Empleado { get; set; } = null!;

    public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();

}
