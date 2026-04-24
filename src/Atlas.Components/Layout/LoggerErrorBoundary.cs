using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Atlas.Components.Layout;
public class LoggerErrorBoundary : ErrorBoundary
{
    [Inject] ILogger<LoggerErrorBoundary> Logger { get; set; } = null!;
    [Inject] ISnackbar Snackbar { get; set; } = null!;
    protected override async Task OnErrorAsync(Exception exception)
    {
        // 1. Logueamos el error
        Logger.LogError(exception, "Error capturado en el sistema.");

        // 2. Mostramos notificación amigable al usuario
        Snackbar.Add("Ha ocurrido un error inesperado. El equipo técnico ha sido notificado.",
            Severity.Error,
            config => {
                config.VisibleStateDuration = 10000; // 10 segundos
                config.ShowCloseIcon = true;
            });


        await base.OnErrorAsync(exception);
    }
}
