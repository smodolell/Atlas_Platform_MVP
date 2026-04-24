using Atlas.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Atlas.Client.Extensions;

public static class HostExtensions
{
    public static async Task RunInitializersAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var initializers = scope.ServiceProvider.GetServices<IAppInitializer>();

        foreach (var initializer in initializers)
        {
            try
            {
                await initializer.InitializeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en initializer: {ex.Message}");
            }
        }
    }
}
