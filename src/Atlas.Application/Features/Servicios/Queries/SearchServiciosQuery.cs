using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Queries;

public class SearchServiciosQuery : IQuery<Result<List<ServicioSearchDto>>>
{
    public string? SearchTerm { get; set; }
    public int MaxResults { get; set; } = 10;
}

internal class SearchServiciosQueryHandler(IAtlasDbContext context) : IQueryHandler<SearchServiciosQuery, Result<List<ServicioSearchDto>>>
{
    public async Task<Result<List<ServicioSearchDto>>> HandleAsync(SearchServiciosQuery request, CancellationToken cancellationToken = default)
    {
        var maxResults = Math.Min(request.MaxResults, 20);
        var term = request.SearchTerm?.Trim().ToLower();

        var result = await context.Servicios
            .Where(s => s.Activo && (string.IsNullOrEmpty(term) || s.NomServicio.ToLower().Contains(term) || s.Descripcion.ToLower().Contains(term)))
            .OrderBy(s => s.NomServicio)
            .Select(s => new ServicioSearchDto
            {
                Id = s.Id,
                NomServicio = s.NomServicio,
                Descripcion = s.Descripcion,
                Activo = s.Activo
            })
            .Take(maxResults)
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}
