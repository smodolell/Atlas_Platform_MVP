namespace Atlas.Application.Features.SelectLists.Queries;

public class GetPlanSelectListQuery : SelectListQueryBase
{
}



internal class GetPlanSelectListQueryHandler(IAtlasDbContext context) : IQueryHandler<GetPlanSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IAtlasDbContext _context = context;


    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetPlanSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.Planes
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomPlan
            }).ToListAsync();

        return Result.Success(items);

    }
}