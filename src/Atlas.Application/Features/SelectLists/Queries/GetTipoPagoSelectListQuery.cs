using Atlas.Shared.Common;

namespace Atlas.Application.Features.SelectLists.Queries;

public class GetTipoPagoSelectListQuery : SelectListQueryBase { }

internal class GetTipoPagoSelectListQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetTipoPagoSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(
        GetTipoPagoSelectListQuery message,
        CancellationToken cancellationToken = default)
    {
        var items = await _context.TiposPago
            .Where(t => t.Activo)
            .OrderBy(t => t.NomTipoPago)
            .Select(t => new SelectListItemDto
            {
                Value = t.Id.ToString(),
                Text = t.NomTipoPago
            })
            .ToListAsync(cancellationToken);

        return Result.Success(items);
    }
}
