namespace Atlas.Shared.Servicios;

public class ServicioSearchDto
{
    public int Id { get; set; }
    public string NomServicio { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
