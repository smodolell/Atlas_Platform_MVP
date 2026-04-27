using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Queries;

public class GetServiciosQuery : IQuery<Result<PagedResultDto<ServicioListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(ServicioListItemDto.Id),
        nameof(ServicioListItemDto.NomServicio),
        nameof(ServicioListItemDto.Activo)
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(ServicioListItemDto.NomServicio);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(ServicioListItemDto.NomServicio);
    }

    public bool SortDescending { get; set; }
    public string? SearchText { get; set; }
}

internal class GetServiciosQueryHandler(IAtlasDbContext context, IPaginator paginator, IDynamicSorter sorter)
    : IQueryHandler<GetServiciosQuery, Result<PagedResultDto<ServicioListItemDto>>>
{
    public async Task<Result<PagedResultDto<ServicioListItemDto>>> HandleAsync(GetServiciosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = context.Servicios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var term = request.SearchText.Trim().ToLower();
                query = query.Where(s => s.NomServicio.ToLower().Contains(term) || s.Descripcion.ToLower().Contains(term));
            }

            var sorted = sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await paginator.PaginateAsync<Servicio, ServicioListItemDto>(sorted, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
