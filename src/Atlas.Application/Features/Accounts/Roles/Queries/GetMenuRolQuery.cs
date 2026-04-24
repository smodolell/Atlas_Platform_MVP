using Atlas.Shared.Accounts.Roles;

namespace Atlas.Application.Features.Accounts.Roles.Queries;

public class GetMenuRolQuery : IQuery<Result<List<MenuTreeItemDto>>>
{
    public required int ApplicationId { get; set; }
    public required int RolId { get; set; }
}

public class GetMenuRolQueryHandler(
    IAtlasDbContext context
) : IQueryHandler<GetMenuRolQuery, Result<List<MenuTreeItemDto>>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<List<MenuTreeItemDto>>> HandleAsync(GetMenuRolQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _context.SYS_Menu
                .Where(r => r.AccessPoint.Any(a => a.ApplicationId == message.ApplicationId))
                .Select(s => new MenuTreeItemDto
                {
                    MenuName = s.Name,
                    MenuId = s.Id,
                    MenuIcon = s.Icon,
                })
                .OrderBy(o => o.MenuName)
                .ToListAsync(cancellationToken);

            foreach (var item in result)
            {
                item.Childs = _context.SYS_AccessPoint
                    .Where(r => r.MenuId == item.MenuId && r.ApplicationId == message.ApplicationId)
                    .Select(s => new MenuTreeItemDto
                    {
                        MenuName = s.AccessPointName,
                        Id = s.Id,
                        IsChecked = _context.SYS_RolAccessPoint.Any(r => r.AccessPointId == s.Id && r.RolId == message.RolId),
                    })
                    .OrderBy(o => o.MenuName)
                    .ToList();

                item.IsChecked = item.Childs.All(a => a.IsChecked);
            }

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
