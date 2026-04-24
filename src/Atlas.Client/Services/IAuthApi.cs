using Atlas.Shared.Accounts.Usuarios;
using Atlas.Shared.Auth;
using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;
public interface IAuthApi
{
    #region Auth

    [Post("/api/auth/login")]
    Task<ApiResponseDto<UsuarioLoginDto>> Login([Body] LoginCommand model);

    [Get("/api/auth/validate-token")]
    Task<ApiResponseDto<TokenValidationDto>> ValidateToken([Query] string token);

    #endregion

  
}
