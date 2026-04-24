namespace Atlas.Domain.Entities;

public class Periodicidad
{
    
    public int Id { get; set; }
    public string NomPeriodicidad { get; set; } = string.Empty;

    public bool Activa { get; set; }

    public ICollection<Producto> Productos { get; set; } = new HashSet<Producto>();
    
}