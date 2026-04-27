namespace Atlas.Shared.Configuracion;

public class PlanHorarioListItemDto
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public int EmpleadoId { get; set; }
    public string NomEmpleado { get; set; } = string.Empty;
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public bool Activo { get; set; }
}
