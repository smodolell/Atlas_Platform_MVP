using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.Asistencias.Commands;
using Atlas.Application.Features.Asistencias.Queries;
using Atlas.Shared.Asistencias;
using Atlas.Shared.Common;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Asistencias : EndpointGroupBase
{
    public override string? GroupName => "asistencias";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Asistencias");

        group.MapGet("", GetAsistenciasAsync)
            .WithSummary("Obtiene asistencias paginadas por fecha")
            .Produces<ApiResponseDto<PagedResultDto<AsistenciaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("socio-status/{socioId:guid}", GetAsistenciaSocioStatusAsync)
            .WithSummary("Obtiene el estado de asistencia actual de un socio")
            .Produces<ApiResponseDto<AsistenciaSocioStatusDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("entrada/", RegistrarEntradaAsync)
            .WithName("RegistrarEntrada")
            .WithSummary("Registra la entrada de un socio")
            .Accepts<RegistrarEntradaDto>("application/json")
            .Produces<ApiResponseDto<Guid>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("salida/", RegistrarSalidaAsync)
            .WithName("RegistrarSalida")
            .WithSummary("Registra la salida de un socio")
            .Accepts<RegistrarSalidaDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetAsistenciasAsync(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 20,
        [FromQuery] string sortColumn = nameof(AsistenciaListItemDto.FechaHoraEntrada),
        [FromQuery] bool sortDescending = true,
        [FromQuery] DateTime? fecha = null,
        [FromQuery] Guid? socioId = null)
    {
        var result = await queryMediator.QueryAsync(new GetAsistenciasQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending,
            Fecha = fecha,
            SocioId = socioId
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetAsistenciaSocioStatusAsync(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid socioId)
    {
        var result = await queryMediator.QueryAsync(new GetAsistenciaSocioStatusQuery(socioId));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> RegistrarEntradaAsync(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] RegistrarEntradaDto model)
    {
        var result = await commandMediator.SendAsync(new RegistrarEntradaCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> RegistrarSalidaAsync(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] RegistrarSalidaDto model)
    {
        var result = await commandMediator.SendAsync(new RegistrarSalidaCommand(model));
        return result.ToCustomMinimalApiResult();
    }
}
