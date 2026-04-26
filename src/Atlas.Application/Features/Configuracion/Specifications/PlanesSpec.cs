namespace Atlas.Application.Features.Configuracion.Specifications;


public class PlanesSpec : Specification<Plan>
{
    public PlanesSpec(string? searchText, int? periodicidadId)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomPlan.Contains(searchText) ||
                p.Descripcion.Contains(searchText));
        }
        if (periodicidadId != null)
        {
            Query.Where(p => p.PeriodicidadId == periodicidadId.Value);
        }

        Query.Include(p => p.Periodicidad);
    }
}
