using DocumentFormat.OpenXml.Office2016.Excel;

namespace Atlas.Application.Features.Socios.Specifications;

public class SociosSpec : Specification<Socio>
{
    public SociosSpec(string? searchText)
    {
        if(!string.IsNullOrEmpty(searchText))
        {
            var searchTerm = searchText.Trim().ToLower();
            Query.Where(s =>s.Nombre.Contains(searchText) || s.Apellido.Contains(searchText) || s.DNI.Contains(searchText));
        }
    }
}
