using Atlas.Application.Features.Configuracion.Specifications;
using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;


public class GetPlanesQuery : IQuery<Result<PagedResultDto<PlanListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(PlanListItemDto.Id),
        nameof(PlanListItemDto.NomPlan),
        nameof(PlanListItemDto.Descripcion),
        nameof(PlanListItemDto.Precio),
        nameof(PlanListItemDto.CupoMaximo),
        nameof(PlanListItemDto.Activo),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(PlanListItemDto.NomPlan);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(PlanListItemDto.NomPlan);
    }

    public bool SortDescending { get; set; }
    public string? SearchText { get; set; }

    public int? PeriodicidadId { get; set; }
}

internal class GetPlanesQueryHandler(
    IAtlasDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetPlanesQuery, Result<PagedResultDto<PlanListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<PlanListItemDto>>> HandleAsync(GetPlanesQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new PlanesSpec(request.SearchText,request.PeriodicidadId);
            var query = _context.Planes.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await _paginator.PaginateAsync<Plan, PlanListItemDto>(sortedQuery, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}