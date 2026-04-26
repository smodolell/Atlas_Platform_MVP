namespace Atlas.Application.Features.Catalogos.Specifications;

public class TiposPagoSpec : Specification<TipoPago>
{
    public TiposPagoSpec(string? searchText)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(t => t.NomTipoPago.Contains(searchText));
        }
    }
}
