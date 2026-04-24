using Atlas.Shared.Common;

namespace Atlas.Application.Features.SelectLists.Queries;

public class GetPeriodicidadSelectListQuery : SelectListQueryBase
{
}



internal class GetPeriodicidadSelectListQueryHandler(IAtlasDbContext context) : IQueryHandler<GetPeriodicidadSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;


    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetPeriodicidadSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.Periodicidades
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomPeriodicidad
            }).ToListAsync();

        return Result.Success(items);

    }
}