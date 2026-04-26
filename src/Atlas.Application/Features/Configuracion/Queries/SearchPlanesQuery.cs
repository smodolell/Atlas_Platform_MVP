using Atlas.Application.Features.Configuracion.Specifications;
using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;

public class SearchPlanesQuery : IQuery<Result<List<PlanSearchDto>>>
{
    public string? SearchTerm { get; set; }
    public int MaxResults { get; set; } = 10;
}

internal class SearchPlanesQueryHandler(IAtlasDbContext context) : IQueryHandler<SearchPlanesQuery, Result<List<PlanSearchDto>>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<List<PlanSearchDto>>> HandleAsync(SearchPlanesQuery request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.SearchTerm) || request.SearchTerm.Length < 2)
            return Result.Success(new List<PlanSearchDto>());

        var maxResults = Math.Min(request.MaxResults, 20);

        var spec = new PlanesSpec(request.SearchTerm, null);
        var result = await _context.Planes
            .WithSpecification(spec)
            .OrderBy(p => p.NomPlan)
            .Select(p => new PlanSearchDto
            {
                Id = p.Id,
                NomPlan = p.NomPlan,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Activo = p.Activo
            })
            .Take(maxResults)
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}
