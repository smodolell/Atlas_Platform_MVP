using Atlas.Domain.Enums;

namespace Atlas.Domain.Entities;

public class Asistencia
{
    public Guid Id { get; set; }
    public int PlanId { get; set; }
    public Guid SocioId { get; set; }
    public Guid? PlanSesionId { get; set; }
    public EstatusAsistencia Estatus { get; set; }
    public DateTime FechaHoraEntrada { get; set; }
    public DateTime? FechaHoraSalida { get; set; }
    public Socio Socio { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
    public PlanSesion? Sesion { get; set; } = null!;
}
