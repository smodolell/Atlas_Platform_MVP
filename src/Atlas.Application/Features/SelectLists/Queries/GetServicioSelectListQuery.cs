namespace Atlas.Application.Features.SelectLists.Queries;

public class GetServicioSelectListQuery : SelectListQueryBase { }

internal class GetServicioSelectListQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetServicioSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetServicioSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var query = context.Servicios.Where(s => s.Activo);

        if (!string.IsNullOrWhiteSpace(message.SearchTerm))
        {
            var term = message.SearchTerm.Trim().ToLower();
            query = query.Where(s => s.NomServicio.ToLower().Contains(term));
        }

        if (message.MaxResults.HasValue)
            query = query.Take(message.MaxResults.Value);

        var items = await query
            .OrderBy(s => s.NomServicio)
            .Select(s => new SelectListItemDto { Value = s.Id.ToString(), Text = s.NomServicio })
            .ToListAsync(cancellationToken);

        return Result.Success(items);
    }
}
