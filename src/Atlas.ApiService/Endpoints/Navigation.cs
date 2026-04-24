using Atlas.ApiService.Infrastructure;
using Atlas.Shared.Common;
using Atlas.Application.Features.Navegation.Commands;
using Atlas.Application.Features.Navegation.Queries;
using Atlas.Shared.Navegation;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Navigation : EndpointGroupBase
{
    public override string? GroupName => "navegation";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Navegation");

        #region Navbar
        group.MapGet("navbar", GetNavbar)
            .WithName("GetNavbar")
            .WithSummary("Obtiene el menú de navegación (navbar) del usuario")
            .WithDescription("Retorna la estructura completa del menú lateral izquierdo con sus respectivos hijos")
            .Produces<ApiResponseDto<HashSet<AccessPointDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Sync

        group.MapPost("sync", SyncAccessPoint)
            .WithName("SyncAccessPoint")
            .WithSummary("Sincroniza los puntos de acceso detectados por reflexión en el cliente")
            .Accepts<ApplicationDto>("application/json")
            .Produces<Result>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto<object>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<object>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<object>>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region Navbar
    public async Task<IResult> GetNavbar(
        [FromServices] IQueryMediator queryMediator)
    {
        var result = await queryMediator.QueryAsync(new GetNavbarQuery());
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Sync

    public async Task<IResult> SyncAccessPoint(
      [FromServices] ICommandMediator commandMediator,
      [FromBody] ApplicationDto model)
    {
        var command = new SyncAccessPointCommand(model);
        var result = await commandMediator.SendAsync(command);
        return result.ToCustomMinimalApiResult();
    }

    #endregion
}