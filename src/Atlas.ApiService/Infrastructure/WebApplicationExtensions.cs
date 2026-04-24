using Atlas.ApiService.Infrastructure;

namespace Atlas.ApiService.Infrastructure;
public static class WebApplicationExtensions
{
    private static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GroupName ?? group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithTags(groupName);
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        // Buscamos en todos los assemblies cargados en el dominio 
        // (o puedes filtrar por nombre: a.FullName.StartsWith("Yggdrasil"))
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var endpointGroupTypes = assemblies
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            // Usamos Try/Catch o validamos que tenga un constructor sin parámetros
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app.MapGroup(instance));
            }
        }


        return app;
    }
}
