using Atlas.Client.Auth;
using Atlas.Client.Handlers;
using Atlas.Client.Initializers;
using Atlas.Client.Interfaces;
using Atlas.Client.Services;
using Atlas.Shared;
using Atlas.Shared.Common;
using Blazilla.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Atlas.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAtlasClient(
        this IServiceCollection services,
        string apiBaseAddress)
    {

        services.AddValidatorsFromAssemblyContaining<AtlasSharedMarker>();

        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

        services.AddTransient<AltasHeaderHandler>();
        services.AddTransient<ErrorDelegatingHandler>();

        AddAtlasRefitClient<IAuthApi>(services, apiBaseAddress);
        AddAtlasRefitClient<INavigationApi>(services, apiBaseAddress);
        AddAtlasRefitClient<IAccountsApi>(services, apiBaseAddress);
        AddAtlasRefitClient<ISociosApi>(services, apiBaseAddress);
        AddAtlasRefitClient<IConfiguracionApi>(services, apiBaseAddress);
        AddAtlasRefitClient<ICatalogosApi>(services, apiBaseAddress);
        AddAtlasRefitClient<ISelectListsApi>(services, apiBaseAddress);
        AddAtlasRefitClient<IAsistenciasApi>(services, apiBaseAddress);




        services.AddScoped<IAppInitializer, NavigationSyncInitializer>();
        return services;
    }


    private static IHttpClientBuilder AddAtlasRefitClient<T>(
        IServiceCollection services,
        string apiBaseAddress
    ) where T : class
    {
        return services.AddRefitClient<T>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseAddress))
            .AddHttpMessageHandler<AltasHeaderHandler>()
            .AddHttpMessageHandler<ErrorDelegatingHandler>();
    }
}


public static class YggdrasilExtensions
{

    public static IServiceCollection AddYggdrasilApplication(this IServiceCollection services, Action<AppSettingDto> configureOptions)
    {
        // Configura las opciones en el contenedor de servicios
        services.Configure(configureOptions);

        // Registra AppConfig como Singleton usando las opciones configuradas
        services.AddSingleton<AppSettingDto>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ApplicationOptions>>().Value;
            return new AppSettingDto(options);
        });

        return services;
    }






}