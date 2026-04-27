namespace Atlas.Shared.Servicios;

public class ServicioHorarioListItemDto
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public int EmpleadoId { get; set; }
    public string NomEmpleado { get; set; } = string.Empty;
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public bool Activo { get; set; }
}
