namespace Atlas.Application.Features.Configuracion.Specifications;


public class ProductosSpec : Specification<Producto>
{
    public ProductosSpec(string? searchText, int? periodicidadId)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomProducto.Contains(searchText) ||
                p.Descripcion.Contains(searchText));
        }
        if (periodicidadId != null)
        {
            Query.Where(p => p.PeriodicidadId == periodicidadId.Value);
        }
    }
}
