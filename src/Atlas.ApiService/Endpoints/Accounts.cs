using Atlas.ApiService.Infrastructure;
using Atlas.Shared.Common;
using Atlas.Application.Features.Accounts.Roles.Commands;
using Atlas.Application.Features.Accounts.Roles.Queries;
using Atlas.Application.Features.Accounts.Usuarios.Commands;
using Atlas.Application.Features.Accounts.Usuarios.Queries;
using Atlas.Shared.Accounts.Roles;
using Atlas.Shared.Accounts.Usuarios;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Accounts : EndpointGroupBase
{
    public override string? GroupName => "accounts";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
          .WithTags("Accounts");

        #region Rol
        group.MapGet("rol/{id}", GetRolById)
            .WithName("GetRolById")
            .WithSummary("Obtiene un rol por ID")
            .Produces<ApiResponseDto<RolUpdateDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("rol/", GetRoles)
            .WithSummary("Obtiene roles paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<RolListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("rol/", CreateRol)
            .WithName("CreateRol")
            .WithSummary("Crea un nuevo rol")
            .Accepts<RolCreateDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("rol/{id}", UpdateRol)
            .WithName("UpdateRol")
            .WithSummary("Actualiza un rol")
            .Accepts<RolUpdateDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("rol/{id}", DeleteRol)
            .WithName("DeleteRol")
            .WithSummary("Elimina un rol")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPatch("rol/{id}/active", ChangeRolActive)
            .WithName("ChangeRolActive")
            .WithSummary("Activa o desactiva un rol")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("rol/{id}/{applicationId}/menu", GetMenuRol)
            .WithName("GetMenuRol")
            .WithSummary("Obtiene el árbol de menú de un rol")
            .Produces<ApiResponseDto<List<MenuTreeItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("rol/{id}/menu", SaveMenuRol)
            .WithName("SaveMenuRol")
            .WithSummary("Guarda los permisos de menú de un rol")
            .Accepts<List<MenuTreeItemDto>>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Usuario
        group.MapGet("usuario/{id}", GetUsuarioById)
            .WithName("GetUsuarioById")
            .WithSummary("Obtiene un usuario por ID")
            .Produces<ApiResponseDto<UsuarioEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("usuario/", GetUsuarios)
            .WithSummary("Obtiene usuarios paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<UsuarioListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("usuario/", CreateUsuario)
            .WithName("CreateUsuario")
            .WithSummary("Crea un nuevo usuario")
            .Accepts<UsuarioCreateDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("usuario/{id}", UpdateUsuario)
            .WithName("UpdateUsuario")
            .WithSummary("Actualiza un usuario")
            .Accepts<UsuarioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("usuario/{id}", DeleteUsuario)
            .WithName("DeleteUsuario")
            .WithSummary("Elimina un usuario")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("usuario/{id}/roles", GetRolesByUsuarioId)
            .WithName("GetRolesByUsuarioId")
            .WithSummary("Obtiene los roles de un usuario")
            .Produces<ApiResponseDto<List<UsuarioRolDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("usuario/{id}/roles", SaveUsuarioRol)
            .WithName("SaveUsuarioRol")
            .WithSummary("Guarda los roles de un usuario")
            .Accepts<Dictionary<int, bool>>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("usuario/change-password", ChangeUserPasswordByAdmin)
            .WithName("ChangeUserPasswordByAdmin")
            .WithSummary("Cambia la contraseña de un usuario (admin)")
            .Accepts<UserChangePasswordDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }
    #region Rol
    public async Task<IResult> GetRolById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetRolByIdQuery { RolId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetRoles(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var result = await queryMediator.QueryAsync(new GetRolesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateRol(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] RolCreateDto model)
    {
        var result = await commandMediator.SendAsync(new CreateRolCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateRol(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] RolUpdateDto model)
    {
        model.RolId = id;
        var result = await commandMediator.SendAsync(new UpdateRolCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteRol(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteRolCommand { RolId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> ChangeRolActive(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromQuery] bool isEnabled)
    {
        var result = await commandMediator.SendAsync(new ChangeRolActiveCommand { RolId = id, IsEnabled = isEnabled });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetMenuRol(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int applicationId,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetMenuRolQuery { ApplicationId = applicationId, RolId = id});
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> SaveMenuRol(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] List<MenuTreeItemDto> model)
    {
        var result = await commandMediator.SendAsync(new SaveMenuRolCommand { RolId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Usuario
    public async Task<IResult> GetUsuarioById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetUsuarioByIdQuery { UsuarioId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetUsuarios(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var result = await queryMediator.QueryAsync(new GetUsuariosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateUsuario(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] UsuarioCreateDto model)
    {
        var result = await commandMediator.SendAsync(new CreateUsuarioCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateUsuario(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] UsuarioEditDto model)
    {
        model.UsuarioId = id;
        var result = await commandMediator.SendAsync(new UpdateUsuarioCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteUsuario(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteUsuarioCommand { UsuarioId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetRolesByUsuarioId(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetRolesByUsuarioIdQuery { UsuarioId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> SaveUsuarioRol(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] Dictionary<int, bool> data)
    {
        var result = await commandMediator.SendAsync(new SaveUsuarioRolCommand { UsuarioId = id, Data = data });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> ChangeUserPasswordByAdmin(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] UserChangePasswordDto model)
    {
        var result = await commandMediator.SendAsync(new ChangeUserPasswordByAdminCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
