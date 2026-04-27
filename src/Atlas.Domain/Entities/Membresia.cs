namespace Atlas.Domain.Entities;

public class Membresia
{
    public Guid Id { get; set; }
    public Guid SocioId { get; set; }
    public int PlanId { get; set; }

    public bool EsLibre { get; set; }
    public bool EsProgramado { get; set; }
    public bool EsTicket { get; set; }

    public int TicketTotal { get; set; }
    public int TicketDisponibles { get; set; }

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



    public Socio Socio { get; set; } = null!;
    public Plan Plan { get; set; } = null!;

    public ICollection<MembresiaPago> MembresiaPagos { get; set; } = new HashSet<MembresiaPago>();
}