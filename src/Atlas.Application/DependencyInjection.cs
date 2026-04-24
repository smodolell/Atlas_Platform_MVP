using Atlas.Shared;
using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<AtlasApplicationMarker>();
        services.AddValidatorsFromAssemblyContaining<AtlasSharedMarker>();

        services.AddLiteBus(configuration =>
        {
            var assembly = typeof(DependencyInjection).Assembly;

            configuration.AddCommandModule(m => m.RegisterFromAssembly(assembly));
            configuration.AddQueryModule(m => m.RegisterFromAssembly(assembly));

        });



        return services;
    }
}
public class AtlasApplicationMarker { }