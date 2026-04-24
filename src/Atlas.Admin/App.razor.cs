using Atlas.Client.Auth;
using Atlas.Client.Extensions;
using Atlas.Components.States;
using Atlas.Components.UserAvatar.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Reflection;

namespace Atlas.Admin;

public partial class App
{
    [Inject] AuthenticationStateProvider AuthState { get; set; } = null!;
    [Inject] AppState AppState { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthState.GetAuthenticationStateAsync();
        var user = authState.User;

        AppState.SetUser(
            user.GetUserName(),
            user.GetFullName()
        );
    }



    public static List<Assembly> AdditionalAssemblies = new List<Assembly>()
    {
        typeof(Atlas.Components._Imports).Assembly,
    };
}
