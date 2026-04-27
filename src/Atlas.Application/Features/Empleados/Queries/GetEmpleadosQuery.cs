using Atlas.Shared.Empleados;
using Atlas.Application.Features.Empleados.Specifications;
using Atlas.Application.Common.Interfaces;
using Ardalis.Result;
using Atlas.Shared.Common;

namespace Atlas.Application.Features.Empleados.Queries;

public class GetEmpleadosQuery : IQuery<Result<PagedResultDto<EmpleadoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(EmpleadoListItemDto.Id),
        nameof(EmpleadoListItemDto.Nombre),
        nameof(EmpleadoListItemDto.Apellido)
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(EmpleadoListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(EmpleadoListItemDto.Id);
    }

    public bool SortDescending { get; set; }
    public string? SearchText { get; set; }
}

internal class GetEmpleadosQueryHandler(
    IAtlasDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetEmpleadosQuery, Result<PagedResultDto<EmpleadoListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<EmpleadoListItemDto>>> HandleAsync(GetEmpleadosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new EmpleadoFiltersSpec(request.SearchText);
            var query = _context.Empleados.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await _paginator.PaginateAsync<Domain.Entities.Empleado, EmpleadoListItemDto>(sortedQuery, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
