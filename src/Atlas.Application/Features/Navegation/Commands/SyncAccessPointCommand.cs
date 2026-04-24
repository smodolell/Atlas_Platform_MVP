using Atlas.Shared.Common;
using Atlas.Shared.Navegation;

namespace Atlas.Application.Features.Navegation.Commands;

public record SyncAccessPointCommand(ApplicationDto Model) : ICommand<Result>;

internal class SyncAccessPointCommandHandler(IAtlasDbContext context) : ICommandHandler<SyncAccessPointCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(SyncAccessPointCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var oApplication = await _context.SYS_Application.SingleOrDefaultAsync(r => r.Id == model.ApplicationId);
        if (oApplication == null)
        {
            oApplication = new Domain.Entities.Application { Id = model.ApplicationId };
            _context.SYS_Application.Add(oApplication);
        }
        oApplication.ApplicationName = model.ApplicationName;
        await _context.SaveChangesAsync();


        // Mantendremos una lista de rutas procesadas para saber cuáles borrar al final
        var processedRoutes = new List<string>();

      
        foreach (var page in model.Pages)
        {
            // Sincronizar Menú
            var oRootMenu = await _context.SYS_Menu.SingleOrDefaultAsync(r => r.Name == page.Menu);
            if (oRootMenu == null)
            {
                oRootMenu = new Menu { Name = page.Menu };
                _context.SYS_Menu.Add(oRootMenu);
                await _context.SaveChangesAsync();
            }
            oRootMenu.Icon = page.MenuIcon;

            // Sincronizar Punto de Acceso (AccessPoint)
            var oAccessPoint = await _context.SYS_AccessPoint.SingleOrDefaultAsync(r =>
                r.ApplicationId == oApplication.Id &&
                r.Route == page.Route);

            if (oAccessPoint == null)
            {
                oAccessPoint = new AccessPoint
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = oApplication.Id,
                    MenuId = oRootMenu.Id,
                    Route = page.Route,
                    Order = 1
                };
                await _context.SYS_AccessPoint.AddAsync(oAccessPoint);
            }

            oAccessPoint.MenuId = oRootMenu.Id;
            oAccessPoint.AccessPointName = page.MenuItem;
            oAccessPoint.IsAnonymous = page.IsAnonymous;

            // Asignar tipo según el Enum (Usando tus constantes)
            oAccessPoint.AccessPointTypeId = page.AccessPointType switch
            {
                Shared.Navegation.AccessPointType.LeftMenu => AppConstants.SYS_AccessPointType_LeftMenu,
                Shared.Navegation.AccessPointType.Page => AppConstants.SYS_AccessPointType_Page,
                Shared.Navegation.AccessPointType.Element => AppConstants.SYS_AccessPointType_Element,
                _ => oAccessPoint.AccessPointTypeId
            };

            await _context.SaveChangesAsync();
            processedRoutes.Add(oAccessPoint.Route);
            var hasAccess = await _context.SYS_RolAccessPoint.AnyAsync(r => r.AccessPointId == oAccessPoint.Id && r.RolId == 1);
            if (!hasAccess)
            {
                _context.SYS_RolAccessPoint.Add(new RolAccessPoint
                {
                    AccessPointId = oAccessPoint.Id,
                    RolId = 1 // rol webmaster
                });
                await _context.SaveChangesAsync();
            }
            
        }
 


        // 6. LIMPIEZA: Borrar Rutas que ya no existen en el código
        var accessPointsToDelete = await _context.SYS_AccessPoint
            .Include(i => i.SYS_RolAccessPoint)
            .Where(r => r.ApplicationId == oApplication.Id && !processedRoutes.Contains(r.Route))
            .ToListAsync();

        foreach (var ap in accessPointsToDelete)
        {
            _context.SYS_RolAccessPoint.RemoveRange(ap.SYS_RolAccessPoint);
            _context.SYS_AccessPoint.Remove(ap);
        }

        await _context.SaveChangesAsync();

        // 7. LIMPIEZA: Menús huérfanos
        var orphanMenus = await _context.SYS_Menu
            .Where(m => !_context.SYS_AccessPoint.Any(ap => ap.MenuId == m.Id))
            .ToListAsync();

        _context.SYS_Menu.RemoveRange(orphanMenus);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}

