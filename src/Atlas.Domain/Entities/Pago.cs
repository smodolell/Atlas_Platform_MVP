namespace Atlas.Domain.Entities;
public class Pago
{
    public Guid Id { get; set; }

    public int TipoPagoId { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal MontoPago { get; set; }

    public TipoPago TipoPago { get; set; } = null!;

    public ICollection<MembresiaPago> MembresiaPagos { get; set; } = new HashSet<MembresiaPago>();
}
