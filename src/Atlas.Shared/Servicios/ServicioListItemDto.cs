namespace Atlas.Shared.Servicios;

public class ServicioListItemDto
{
    public int Id { get; set; }
    public string NomServicio { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
