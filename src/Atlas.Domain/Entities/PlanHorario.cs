namespace Atlas.Domain.Entities;

public class PlanHorario
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public int EmpleadoId { get; set; } // Profesor
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public bool Activo { get; set; }

    public Plan Plan { get; set; } = null!;
    public Empleado Empleado { get; set; } = null!;

}
