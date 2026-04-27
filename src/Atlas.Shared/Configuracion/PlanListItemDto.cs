namespace Atlas.Shared.Configuracion;

public class PlanListItemDto
{
    public int Id { get; set; }
    public string NomPlan { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string NomPeriodicidad { get; set; } = string.Empty;
    public string NomServicio { get; set; } = string.Empty;

    public decimal Precio { get; set; }
    public int CupoMaximo { get; set; }

    public bool EsLibre { get; set; }
    public bool EsProgramado { get; set; }
    public bool EsTicket { get; set; }

    public bool Activo { get; set; }

}
