namespace Atlas.Domain.Entities;

public class Servicio
{
    public int Id { get; set; }
    public string NomServicio { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; }

    // Un servicio puede tener muchos planes comerciales
    public ICollection<Plan> Planes { get; set; } = new List<Plan>();
    public ICollection<ServicioHorario> Horarios { get; set; } = new List<ServicioHorario>();
}
