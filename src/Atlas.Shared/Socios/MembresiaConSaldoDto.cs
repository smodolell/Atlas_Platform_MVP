namespace Atlas.Shared.Socios;

public class MembresiaConSaldoDto
{
    public Guid Id { get; set; }
    public string NomPlan { get; set; } = string.Empty;
    public decimal MontoSaldo { get; set; }
    public decimal IVASaldo { get; set; }
    public decimal TotalSaldo { get; set; }
    public DateTime FechaVencimiento { get; set; }
}
