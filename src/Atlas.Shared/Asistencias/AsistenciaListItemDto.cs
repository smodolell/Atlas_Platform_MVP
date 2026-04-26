namespace Atlas.Shared.Asistencias;

public class AsistenciaListItemDto
{
    public Guid Id { get; set; }
    public string NomSocio { get; set; } = string.Empty;
    public string NomPlan { get; set; } = string.Empty;
    public DateTime FechaHoraEntrada { get; set; }
    public DateTime? FechaHoraSalida { get; set; }
    public string Estatus { get; set; } = string.Empty;
}
