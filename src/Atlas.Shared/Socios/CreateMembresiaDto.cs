namespace Atlas.Shared.Socios;

public class CreateMembresiaDto
{
    public Guid SocioId { get; set; }
    public int ProductoId { get; set; }
    public DateTime? FechaInicio { get; set; } = DateTime.Now;
    public int Duracion { get; set; } = 1;
}
