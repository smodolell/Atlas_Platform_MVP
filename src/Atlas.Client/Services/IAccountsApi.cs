using Ardalis.Result;
using Atlas.Shared.Accounts.Roles;
using Atlas.Shared.Accounts.Usuarios;
using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;


public interface IAccountsApi
{
    #region Rol

    [Get("/api/accounts/rol/{id}")]
    Task<ApiResponseDto<RolUpdateDto>> GetRolById(int id);

    [Get("/api/accounts/rol/")]
    Task<ApiResponseDto<PagedResultDto<RolListItemDto>>> GetRoles(
        string? q = null,
        int page = 1,
        int size = 10);

    [Post("/api/accounts/rol/")]
    Task<Result> CreateRol([Body] RolCreateDto model);

    [Put("/api/accounts/rol/{id}")]
    Task<ApiResponseDto> UpdateRol(int id, [Body] RolUpdateDto model);

    [Delete("/api/accounts/rol/{id}")]
    Task<ApiResponseDto> DeleteRol(int id);

    [Patch("/api/accounts/rol/{id}/active")]
    Task<Result> ChangeRolActive(int id, bool isEnabled);

    [Get("/api/accounts/rol/{id}/{applicationId}/menu")]
    Task<ApiResponseDto<List<MenuTreeItemDto>>> GetMenuRol(int applicationId, int id);

    [Post("/api/accounts/rol/{id}/menu")]
    Task<ApiResponseDto> SaveMenuRol(int id, [Body] List<MenuTreeItemDto> model);

    #endregion

    #region Usuario

    [Get("/api/accounts/usuario/{id}")]
    Task<ApiResponseDto<UsuarioEditDto>> GetUsuarioById(int id);

    [Get("/api/accounts/usuario/")]
    Task<ApiResponseDto<PagedResultDto<UsuarioListItemDto>>> GetUsuarios(
        string? q = null,
        int page = 1,
        int size = 10);

    [Post("/api/accounts/usuario/")]
    Task<ApiResponseDto> CreateUsuario([Body] UsuarioCreateDto model);

    [Put("/api/accounts/usuario/{id}")]
    Task<ApiResponseDto> UpdateUsuario(int id, [Body] UsuarioEditDto model);

    [Delete("/api/accounts/usuario/{id}")]
    Task<Result> DeleteUsuario(int id);

    [Get("/api/accounts/usuario/{id}/roles")]
    Task<ApiResponseDto<List<UsuarioRolDto>>> GetRolesByUsuarioId(int id);

    [Post("/api/accounts/usuario/{id}/roles")]
    Task<ApiResponseDto> SaveUsuarioRol(int id, [Body] Dictionary<int, bool> data);

    [Post("/api/accounts/usuario/change-password")]
    Task<ApiResponseDto> ChangeUserPasswordByAdmin([Body] UserChangePasswordDto model);

    #endregion
}
