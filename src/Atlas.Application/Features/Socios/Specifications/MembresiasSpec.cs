namespace Atlas.Application.Features.Socios.Specifications;

public class MembresiasSpec : Specification<Membresia>
{
    public MembresiasSpec(string? searchText, Guid? socioId = null)
    {
        if (socioId.HasValue)
            Query.Where(m => m.SocioId == socioId.Value);

        if (!string.IsNullOrEmpty(searchText))
            Query.Where(m => m.Socio.Nombre.Contains(searchText) || m.Plan.NomPlan.Contains(searchText));

        Query.Include(m => m.Socio);
        Query.Include(m => m.Plan);
    }
}
