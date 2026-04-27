namespace Atlas.Domain.Entities;

public class ServicioHorario
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public int EmpleadoId { get; set; } // Profesor
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public bool Activo { get; set; }

    public Servicio Servicio { get; set; } = null!;
    public Empleado Empleado { get; set; } = null!;

}