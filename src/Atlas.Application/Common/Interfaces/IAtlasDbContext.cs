namespace Atlas.Application.Common.Interfaces;

public interface IAtlasDbContext
{
    DbSet<Asistencia> Asistencias { get; }
    DbSet<Socio> Socios { get; }
    DbSet<Plan> Planes { get; }
    DbSet<PlanHorario> PlanesHorario { get; }
    DbSet<PlanSesion> PlanesSesion { get; }
    DbSet<Periodicidad> Periodicidades { get; }
    DbSet<Membresia> Membresias { get; }
    DbSet<TipoPago> TiposPago { get; }
    DbSet<Pago> Pagos { get; }
    DbSet<MembresiaPago> MembresiaPagos { get; }
    DbSet<Domain.Entities.Application> SYS_Application { get; }
    DbSet<AccessPoint> SYS_AccessPoint { get; }
    DbSet<RolAccessPoint> SYS_RolAccessPoint { get; }
    DbSet<Menu> SYS_Menu { get; }
    DbSet<Empleado> Empleados { get; }
    DbSet<Servicio> Servicios { get; }
    DbSet<ServicioHorario> ServicioHorarios { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
