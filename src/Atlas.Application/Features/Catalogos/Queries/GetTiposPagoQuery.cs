using Atlas.Application.Features.Catalogos.Specifications;
using Atlas.Shared.Catalogos;

namespace Atlas.Application.Features.Catalogos.Queries;

public class GetTiposPagoQuery : IQuery<Result<PagedResultDto<TipoPagoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TipoPagoListItemDto.Id),
        nameof(TipoPagoListItemDto.NomTipoPago),
        nameof(TipoPagoListItemDto.Activo),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TipoPagoListItemDto.NomTipoPago);

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch { < 1 => 10, > 100 => 100, _ => value };
    }

    public string SortColumn
    {
        get => _sortColumn;
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TipoPagoListItemDto.NomTipoPago);
    }

    public bool SortDescending { get; set; }
    public string? SearchText { get; set; }
}

internal class GetTiposPagoQueryHandler(
    IAtlasDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetTiposPagoQuery, Result<PagedResultDto<TipoPagoListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<TipoPagoListItemDto>>> HandleAsync(GetTiposPagoQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new TiposPagoSpec(request.SearchText);
            var query = _context.TiposPago.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await _paginator.PaginateAsync<TipoPago, TipoPagoListItemDto>(sortedQuery, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
