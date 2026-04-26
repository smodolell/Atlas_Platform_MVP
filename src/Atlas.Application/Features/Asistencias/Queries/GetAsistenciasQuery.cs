using Atlas.Application.Features.Asistencias.Specifications;
using Atlas.Shared.Asistencias;

namespace Atlas.Application.Features.Asistencias.Queries;

public class GetAsistenciasQuery : IQuery<Result<PagedResultDto<AsistenciaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(AsistenciaListItemDto.NomSocio),
        nameof(AsistenciaListItemDto.NomPlan),
        nameof(AsistenciaListItemDto.FechaHoraEntrada),
        nameof(AsistenciaListItemDto.FechaHoraSalida),
    ];

    private int _page = 1;
    private int _pageSize = 20;
    private string _sortColumn = nameof(AsistenciaListItemDto.FechaHoraEntrada);

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch { < 1 => 20, > 100 => 100, _ => value };
    }

    public string SortColumn
    {
        get => _sortColumn;
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(AsistenciaListItemDto.FechaHoraEntrada);
    }

    public bool SortDescending { get; set; } = true;
    public string? SearchText { get; set; }
    public DateTime? Fecha { get; set; }
    public Guid? SocioId { get; set; }
}

internal class GetAsistenciasQueryHandler(
    IAtlasDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetAsistenciasQuery, Result<PagedResultDto<AsistenciaListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<AsistenciaListItemDto>>> HandleAsync(GetAsistenciasQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new AsistenciasSpec(request.SearchText, request.Fecha, request.SocioId);
            var query = _context.Asistencias.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);
            var result = await _paginator.PaginateAsync<Asistencia, AsistenciaListItemDto>(sortedQuery, request.Page, request.PageSize, cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
