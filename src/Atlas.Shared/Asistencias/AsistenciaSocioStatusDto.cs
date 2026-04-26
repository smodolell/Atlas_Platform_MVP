namespace Atlas.Shared.Asistencias;

public class AsistenciaSocioStatusDto
{
    public bool TieneEntradaAbierta { get; set; }
    public Guid? AsistenciaId { get; set; }
    public DateTime? FechaHoraEntrada { get; set; }
    public string NomPlan { get; set; } = string.Empty;
}
