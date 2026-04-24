namespace Atlas.Domain.Entities;

public class Producto
{
    public int Id { get; set; }
    public int PeriodicidadId { get; set; }
    public string NomProducto { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    public decimal Precio { get; set; }
    public int CupoMaximo { get; set; }

    public bool Activo { get; set; }

    public Periodicidad Periodicidad { get; set; } = null!;

    public ICollection<Membresia> Membresias { get; set; } = new HashSet<Membresia>();
}
