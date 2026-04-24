namespace Atlas.Domain.Entities;

public class TipoPago
{
    public int Id { get; set; }
    public string NomTipoPago { get; set; } = string.Empty;

    public bool Activo { get; set; }


    public ICollection<Pago> Pagos { get; set; } = new HashSet<Pago>();
}
