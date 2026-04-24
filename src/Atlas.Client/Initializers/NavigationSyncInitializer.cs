using Atlas.Client.Interfaces;
using Atlas.Client.Services;
using Atlas.Components.Helpers;
using Atlas.Shared.Common;
using Atlas.Shared.Navegation;
using System.Reflection;

namespace Atlas.Client.Initializers;

internal class NavigationSyncInitializer(INavigationApi layoutApi ,AppSettingDto appSetting) : IAppInitializer
{
    private readonly INavigationApi _navegationApi = layoutApi;
    private readonly AppSettingDto _appSetting = appSetting;

    public async Task InitializeAsync()
    {
     
        var assembly = Assembly.GetEntryAssembly();

        if (assembly == null) return;

       
        var pages = Utils.GetPagesFromAssembly(assembly);

        var application = new ApplicationDto
        {
            ApplicationId = _appSetting.ApplicationId,
            ApplicationName = _appSetting.ApplicationName,
            Pages = pages
        };

        await _navegationApi.SyncAccessPoint(application);
    }
}
