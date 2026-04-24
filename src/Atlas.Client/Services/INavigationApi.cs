using Atlas.Shared.Common;
using Atlas.Shared.Navegation;
using Refit;

namespace Atlas.Client.Services;



public interface INavigationApi
{
    #region Navbar

    /// <summary>
    /// Obtiene el menú de navegación (navbar) del usuario
    /// </summary>
    /// <returns>Retorna la estructura completa del menú lateral izquierdo con sus respectivos hijos</returns>
    [Get("/api/navegation/navbar")]
    Task<ApiResponseDto<HashSet<AccessPointDto>>> GetNavbar();

    #endregion

    #region Sync

    /// <summary>
    /// Sincroniza los puntos de acceso detectados por reflexión en el cliente
    /// </summary>
    /// <param name="model">Datos de la aplicación a sincronizar</param>
    /// <returns>Resultado de la sincronización</returns>
    [Post("/api/navegation/sync")]
    Task<ApiResponseDto> SyncAccessPoint([Body] ApplicationDto model);

    #endregion
}
