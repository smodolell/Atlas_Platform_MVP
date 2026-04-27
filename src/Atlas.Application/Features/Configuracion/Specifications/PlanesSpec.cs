namespace Atlas.Application.Features.Configuracion.Specifications;


public class PlanesSpec : Specification<Plan>
{
    public PlanesSpec(string? searchText, int? periodicidadId, int? servicioId)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomPlan.Contains(searchText) ||
                p.Descripcion.Contains(searchText));
        }
        if (periodicidadId != null)
            Query.Where(p => p.PeriodicidadId == periodicidadId.Value);

        if (servicioId != null)
            Query.Where(p => p.ServicioId == servicioId.Value);

        Query.Include(p => p.Periodicidad)
             .Include(p => p.Servicio);
    }
}
