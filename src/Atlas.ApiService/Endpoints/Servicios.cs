using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.Servicios.Commands;
using Atlas.Application.Features.Servicios.Queries;
using Atlas.Shared.Common;
using Atlas.Shared.Servicios;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Servicios : EndpointGroupBase
{
    public override string? GroupName => "servicios";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/").WithTags("Servicios");

        group.MapGet("servicio/search", SearchServicios)
            .WithSummary("Busca servicios por término de búsqueda")
            .Produces<ApiResponseDto<List<ServicioSearchDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapGet("servicio/{id:int}", GetServicioById)
            .WithName("GetServicioById")
            .WithSummary("Obtiene un servicio por ID")
            .Produces<ApiResponseDto<ServicioEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapGet("servicio/", GetServicios)
            .WithSummary("Obtiene servicios paginados")
            .Produces<ApiResponseDto<PagedResultDto<ServicioListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapPost("servicio/", CreateServicio)
            .WithName("CreateServicio")
            .WithSummary("Crea un servicio")
            .Accepts<ServicioEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapPut("servicio/{id:int}", UpdateServicio)
            .WithName("UpdateServicio")
            .WithSummary("Actualiza un servicio")
            .Accepts<ServicioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapDelete("servicio/{id:int}", DeleteServicio)
            .WithName("DeleteServicio")
            .WithSummary("Elimina un servicio")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        // Horarios
        group.MapGet("servicio/{servicioId:int}/horario/", GetHorariosByServicioId)
            .WithName("GetServicioHorariosByServicioId")
            .WithSummary("Obtiene horarios de un servicio")
            .Produces<ApiResponseDto<List<ServicioHorarioListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapPost("servicio/{servicioId:int}/horario/", CreateHorario)
            .WithName("CreateServicioHorario")
            .WithSummary("Crea un horario de servicio")
            .Accepts<ServicioHorarioEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapPut("servicio/horario/{id:int}", UpdateHorario)
            .WithName("UpdateServicioHorario")
            .WithSummary("Actualiza un horario de servicio")
            .Accepts<ServicioHorarioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);

        group.MapDelete("servicio/horario/{id:int}", DeleteHorario)
            .WithName("DeleteServicioHorario")
            .WithSummary("Elimina un horario de servicio")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized);
    }

    public async Task<IResult> SearchServicios(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int maxResults = 10)
    {
        var result = await queryMediator.QueryAsync(new SearchServiciosQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetServicioById(
        [FromServices] IQueryMediator queryMediator, [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetServicioByIdQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetServicios(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(ServicioListItemDto.NomServicio),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetServiciosQuery
        {
            SearchText = q, Page = page, PageSize = size,
            SortColumn = sortColumn, SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateServicio(
        [FromServices] ICommandMediator commandMediator, [FromBody] ServicioEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateServicioCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateServicio(
        [FromServices] ICommandMediator commandMediator, [FromRoute] int id, [FromBody] ServicioEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateServicioCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteServicio(
        [FromServices] ICommandMediator commandMediator, [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteServicioCommand(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetHorariosByServicioId(
        [FromServices] IQueryMediator queryMediator, [FromRoute] int servicioId)
    {
        var result = await queryMediator.QueryAsync(new GetServicioHorariosByServicioIdQuery { ServicioId = servicioId });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateHorario(
        [FromServices] ICommandMediator commandMediator, [FromRoute] int servicioId, [FromBody] ServicioHorarioEditDto model)
    {
        model.ServicioId = servicioId;
        var result = await commandMediator.SendAsync(new CreateServicioHorarioCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateHorario(
        [FromServices] ICommandMediator commandMediator, [FromRoute] int id, [FromBody] ServicioHorarioEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateServicioHorarioCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteHorario(
        [FromServices] ICommandMediator commandMediator, [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteServicioHorarioCommand(id));
        return result.ToCustomMinimalApiResult();
    }
}
