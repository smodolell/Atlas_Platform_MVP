using Atlas.Domain.Enums;

namespace Atlas.Domain.Entities;

public class Plan
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public int PeriodicidadId { get; set; }

    public string NomPlan { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    public bool EsLibre { get; set; }
    public bool EsProgramado { get; set; }
    public bool EsTicket { get; set; }

    public int CupoMaximo { get; set; }

    public int DiasGracia { get; set; }
    public bool Activo { get; set; }

    public Servicio Servicio { get; set; } = null!;
    public Periodicidad Periodicidad { get; set; } = null!;

    public ICollection<PlanSesion> Sesiones { get; set; } = new List<PlanSesion>();
    public ICollection<PlanHorario> Horarios { get; set; } = new HashSet<PlanHorario>();
    public ICollection<Membresia> Membresias { get; set; } = new HashSet<Membresia>();
    public ICollection<Asistencia> Asistencias { get; set; } = new HashSet<Asistencia>();
}
