using Atlas.Application.Common.Interfaces;
using Atlas.Application.Features.Auth.Interfaces;
using Atlas.Domain.Entities;
using Atlas.Infrastructure.Persistence;
using Atlas.Infrastructure.Persistence.Initializers;
using Atlas.Infrastructure.Services;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configuración de Mapster
        MapsterConfig.Configure();

        services.AddMapster();


        // 2. Configuración de Base de Datos
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Atlas.Infrastructure"));
        }, ServiceLifetime.Scoped);

        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        }, ServiceLifetime.Scoped);


        services.AddIdentity<Usuario, Rol>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        ;



        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;

            // User settings
            options.User.RequireUniqueEmail = false;
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IAtlasDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaginator, Paginator>();
        services.AddScoped<IDynamicSorter, DynamicSorter>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddHttpContextAccessor();

        // 2. Tu servicio de identidad
        //services.AddScoped<IUserContext, UserContext>();

        services.AddHostedService<UserInitializer>();


        //services.AddLiteBus(liteBus =>
        //{
        //    liteBus.AddCommandModule(module =>
        //    {
        //        module.Register(typeof(AuditoriaMiddleware<>));
        //    });
        //});
        return services;
    }

}