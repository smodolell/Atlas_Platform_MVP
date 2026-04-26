using Atlas.Application.Common.Interfaces;
using Atlas.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Atlas.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, int>, IAtlasDbContext
{
    public DbSet<Socio> Socios => Set<Socio>();

    public DbSet<Plan> Planes => Set<Plan>();

    public DbSet<Domain.Entities.Application> SYS_Application =>  base.Set<Domain.Entities.Application>();

    public DbSet<AccessPoint> SYS_AccessPoint =>  Set<AccessPoint>();

    public DbSet<RolAccessPoint> SYS_RolAccessPoint =>  Set<RolAccessPoint>();

    public DbSet<Menu> SYS_Menu =>  Set<Menu>();

    public DbSet<Periodicidad> Periodicidades => Set<Periodicidad>();

    public DbSet<Membresia> Membresias => Set<Membresia>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
           .SelectMany(t => t.GetForeignKeys())
           .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
