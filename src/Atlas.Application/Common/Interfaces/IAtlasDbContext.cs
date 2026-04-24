using Atlas.Domain.Entities;

namespace Atlas.Application.Common.Interfaces;

public interface IAtlasDbContext
{
    DbSet<Socio> Socios { get; }
    DbSet<Producto> Productos { get; }
    DbSet<Periodicidad> Periodicidades { get; }
    DbSet<Domain.Entities.Application> SYS_Application { get; }
    DbSet<AccessPoint> SYS_AccessPoint { get; }
    DbSet<RolAccessPoint> SYS_RolAccessPoint { get; }
    DbSet<Menu> SYS_Menu { get; }



    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
