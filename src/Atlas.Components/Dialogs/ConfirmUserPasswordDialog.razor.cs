using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using Atlas.Components.Dialogs;


namespace Yggdrasil.Component.Dialogs;

public partial class ConfirmUserPasswordDialog
{
    private Dictionary<string, object> _inputAttributes =
            new Dictionary<string, object>()
                {
                   { "autocomplete", "new-password2" },
                };

    private bool _isLoading = false;

    private PasswordModel _passwordModel = new PasswordModel();

    [CascadingParameter] IMudDialogInstance? MudDialog { get; set; }

    [Inject] public AuthenticationStateProvider? AuthStateProvider { get; set; }

    [Parameter] public EventCallback<CommonDialogEventArgs> ConfirmCallBack { get; set; }

    private async Task ConfirmPassword()
    {
        _isLoading = true;
        //using var context = await _dbFactory.CreateDbContextAsync();

        //var userId = await AuthStateProvider!.GetUserIdAsync();
        //var user = context.Users.Find(userId);
        //if (user == null)
        //{
        //    _snackbarService.Add("NotFindUser", MudBlazor.Severity.Error);
        //    MudDialog?.Close(DialogResult.Cancel());
        //    return;
        //}
        ////var isPwdValid = HashHelper.VerifyPassword(user.PasswordHash ?? "", _passwordModel.Password!);
        ////if (!isPwdValid)
        ////{
        ////    _snackbarService.Add(PasswordValidFail, Severity.Error);
        ////    MudDialog?.Close(DialogResult.Cancel());
        ////    return;
        ////}

        await ConfirmCallBack.InvokeAsync(new CommonDialogEventArgs());
        _isLoading = false;
        MudDialog?.Close(DialogResult.Ok(true));
    }

    private class PasswordModel
    {
        [Required]
        public string? Password { get; set; }
    }

}
