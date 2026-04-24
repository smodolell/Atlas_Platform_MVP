using Microsoft.AspNetCore.Components;

namespace Atlas.Components.Layout;

public partial class EmptyLayout
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            _appState.OnChange += StateHasChanged;
            await _appState.InitializeAsync();
        }
    }
}
