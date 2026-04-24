using Atlas.Components.States;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace Atlas.Components.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAtlasComponents(this IServiceCollection services)
    {
        // =============================
        // STATES
        // =============================
        services.AddScoped<AppState>();

        // =============================
        // MUD BLAZOR
        // =============================
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = true;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 4000;
        });

        return services;
    }
}
