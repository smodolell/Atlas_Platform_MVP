using Atlas.Shared.Navegation;
using Microsoft.AspNetCore.Components;

namespace Atlas.Components.Layout;

public partial class AuthorizedLayout
{
    [Parameter] public RenderFragment? Child { get; set; }


    [Parameter] public EventCallback OnLogout { get; set; }
    [Parameter] public EventCallback OnSettings { get; set; }
    [Parameter] public HashSet<AccessPointDto>? NavMenuItems { get; set; }

    [Parameter] public EventCallback<AccessPointDto> OnNavigate { get; set; }
}
