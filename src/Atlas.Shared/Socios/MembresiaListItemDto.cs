namespace Atlas.Shared.Socios;

public class MembresiaListItemDto
{
    public Guid Id { get; set; }
    public Guid SocioId { get; set; }
    public int ProductoId { get; set; }

    public string NomPlan { get; set; } = "";
    public string NomSocio { get; set; } = "";
    public decimal Monto { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }

    public decimal MontoSaldo { get; set; }
    public decimal IVASaldo { get; set; }
    public decimal TotalSaldo { get; set; }



    public DateTime FechaInicio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public DateTime FechaFinalizacion { get; set; }
    public int DiasGracia { get; set; }
}
