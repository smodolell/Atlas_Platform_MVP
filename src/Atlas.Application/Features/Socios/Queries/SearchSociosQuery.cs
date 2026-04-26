using Atlas.Application.Features.Socios.Specifications;
using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Queries;

public class SearchSociosQuery : IQuery<Result<List<SocioSearchDto>>>
{
    public string? SearchTerm { get; set; }
    public int MaxResults { get; set; } = 10;
}

internal class SearchSociosQueryHandler(IAtlasDbContext context) : IQueryHandler<SearchSociosQuery, Result< List<SocioSearchDto>>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<List<SocioSearchDto>>> HandleAsync(SearchSociosQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SearchTerm) || request.SearchTerm.Length < 2)
        {
            return new List<SocioSearchDto>();
        }

        var searchTerm = request.SearchTerm.Trim().ToLower();
        var maxResults = Math.Min(request.MaxResults, 20); // Máximo 20 resultados

        var spec = new SociosSpec(request.SearchTerm);
        var query = _context.Socios
            .WithSpecification(spec)
            .OrderBy(s => s.Nombre);
        

        var result = await query
            .Select(s => new SocioSearchDto
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                DNI = s.DNI,
                FechaNacimiento = s.FechaNacimiento,
                Email = s.Email,
                Telefono = s.Telefono
            })
            .Take(maxResults)
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}
