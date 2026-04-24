namespace Atlas.Application.Features.Socios.Specifications;

public class SociosSpec : Specification<Socio>
{
    public SociosSpec(string? searchText)
    {
        if(!string.IsNullOrEmpty(searchText))
        {
            Query.Where(s =>s.Nombre.Contains(searchText) || s.Apellido.Contains(searchText) || s.DNI.Contains(searchText));
        }
    }
}
