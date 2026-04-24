namespace Atlas.Shared.Configuracion;

public class ProductoListItemDto
{
    public int Id { get; set; }
    public string NomProducto { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int CupoMaximo { get; set; }

    public bool Activo { get; set; }

}
