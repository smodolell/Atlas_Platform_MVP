using Atlas.Application.Features.Configuracion.Specifications;
using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;


public class GetProductosQuery : IQuery<Result<PagedResultDto<ProductoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(ProductoListItemDto.Id),
        nameof(ProductoListItemDto.NomProducto),
        nameof(ProductoListItemDto.Descripcion),
        nameof(ProductoListItemDto.Precio),
        nameof(ProductoListItemDto.CupoMaximo),
        nameof(ProductoListItemDto.Activo),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(ProductoListItemDto.NomProducto);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(ProductoListItemDto.NomProducto);
    }

    public bool SortDescending { get; set; }
    public string? SearchText { get; set; }
}

internal class GetProductosQueryHandler(
    IAtlasDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetProductosQuery, Result<PagedResultDto<ProductoListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<ProductoListItemDto>>> HandleAsync(GetProductosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new ProductosSpec(request.SearchText);
            var query = _context.Productos.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await _paginator.PaginateAsync<Producto, ProductoListItemDto>(sortedQuery, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}