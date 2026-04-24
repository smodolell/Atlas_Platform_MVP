using Atlas.Components.States;
using Atlas.Shared.Navegation;
using Microsoft.AspNetCore.Components;

namespace Atlas.Components.NavMenus;

public partial class NavMenu : IDisposable
{

    protected override void OnInitialized()
    {
        _appState.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        _appState.OnChange -= StateHasChanged;
    }

    private void NavTo(AccessPointDto item)
    {
        _appState.NavTo(item);
    }
}