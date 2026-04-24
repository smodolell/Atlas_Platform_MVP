namespace Atlas.Domain.Entities;

public class MembresiaPago
{
    public Guid Id { get; set; }
    public Guid MembresiaId { get; set; }
    public Guid PagoId { get; set; }
    public decimal Monto { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaPago { get; set; }
    public Membresia Membresia { get; set; } = null!;
    public Pago Pago { get; set; } = null!;
}
