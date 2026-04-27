namespace Atlas.Application.Features.SelectLists.Queries;

public class GetEmpleadoSelectListQuery : SelectListQueryBase { }

internal class GetEmpleadoSelectListQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetEmpleadoSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetEmpleadoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var query = context.Empleados.AsQueryable();

        if (!string.IsNullOrWhiteSpace(message.SearchTerm))
        {
            var term = message.SearchTerm.Trim().ToLower();
            query = query.Where(e => e.Nombre.ToLower().Contains(term) || e.Apellido.ToLower().Contains(term));
        }

        if (message.MaxResults.HasValue)
            query = query.Take(message.MaxResults.Value);

        var items = await query
            .OrderBy(e => e.Nombre)
            .ThenBy(e => e.Apellido)
            .Select(e => new SelectListItemDto
            {
                Value = e.Id.ToString(),
                Text = e.Nombre + " " + e.Apellido
            })
            .ToListAsync(cancellationToken);

        return Result.Success(items);
    }
}
