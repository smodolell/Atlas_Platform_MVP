using Atlas.Shared.Navegation;
using Microsoft.AspNetCore.Components;

namespace Atlas.Components.NavMenus;

public partial class NavItemMenu
{
    [Parameter] public HashSet<AccessPointDto> NavMenuItems { get; set; } = new();

    [Parameter] public EventCallback<AccessPointDto> NavTo { get; set; }

    private async Task NavClick(AccessPointDto item)
    {
        await NavTo.InvokeAsync(item);
    }

}

