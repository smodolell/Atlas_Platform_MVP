namespace Atlas.Application.Features.Socios.Specifications;

public class MembresiasSpec :Specification<Membresia>
{
    public MembresiasSpec(string? searchText)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(m => m.Socio.Nombre.Contains(searchText));
        }

        Query.Include(m => m.Socio);
        Query.Include(m => m.Plan);


    }
}
