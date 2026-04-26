using Atlas.Application.Features.Socios.Specifications;
using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Queries;

public class GetMembresiasQuery : IQuery<Result<PagedResultDto<MembresiaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
[
    nameof(MembresiaListItemDto.Id),
        nameof(MembresiaListItemDto.NomPlan),
        nameof(MembresiaListItemDto.NomSocio),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(MembresiaListItemDto.NomPlan);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(SocioListItemDto.Nombre);
    }

    public bool SortDescending { get; set; }
    public string? SearchText { get; set; }
    public Guid? SocioId { get; set; }
}

internal class GetMembresiasQueryHandler(
    IAtlasDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetMembresiasQuery, Result<PagedResultDto<MembresiaListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<MembresiaListItemDto>>> HandleAsync(GetMembresiasQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new MembresiasSpec(request.SearchText, request.SocioId);
            var query = _context.Membresias.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await _paginator.PaginateAsync<Membresia, MembresiaListItemDto>(sortedQuery, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
