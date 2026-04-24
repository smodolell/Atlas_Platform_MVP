using Atlas.Shared.Common;
using Atlas.Shared.Navegation;

namespace Atlas.Application.Features.Navegation.Queries;

public class GetNavbarQuery : IQuery<Result<HashSet<AccessPointDto>>>
{
    public int ApplicationId { get; set; }
}

internal class GetNavbarQueryHandler(IAtlasDbContext context) : IQueryHandler<GetNavbarQuery, Result<HashSet<AccessPointDto>>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<HashSet<AccessPointDto>>> HandleAsync(GetNavbarQuery message, CancellationToken cancellationToken = default)
    {
        var applicationId = message.ApplicationId;
        var menus = await _context.SYS_Menu
          .Where(r => r.AccessPoint.Any(r =>r.AccessPointTypeId == AppConstants.SYS_AccessPointType_LeftMenu))
          .Select(s => new AccessPointDto
          {

              MenuIcon = s.Icon,
              MenuName = s.Name,
              Childs = s.AccessPoint.Where(r => r.AccessPointTypeId == AppConstants.SYS_AccessPointType_LeftMenu)
              .Select(s1 => new AccessPointDto
              {

                  MenuIcon = s1.Icon,
                  MenuName = s1.AccessPointName,
                  Route = s1.Route,
              })
              .OrderBy(o => o.MenuName)
              .ToHashSet(),

          })
          .OrderBy(o => o.MenuName)
          .ToHashSetAsync();

        return Result.Success(menus);

    }
}
