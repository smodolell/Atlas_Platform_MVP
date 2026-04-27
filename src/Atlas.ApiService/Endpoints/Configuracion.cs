using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.Configuracion.Commands;
using Atlas.Application.Features.Configuracion.Queries;
using Atlas.Shared.Common;
using Atlas.Shared.Configuracion;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Configuracion : EndpointGroupBase
{
    public override string? GroupName => "configuracion";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
          .WithTags("Configuración");

        group.MapGet("plan/{id}", GetPlanById)
            .WithName("GetPlanById")
            .WithSummary("Obtiene un plan por ID")
            .Produces<ApiResponseDto<PlanEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("plan/", GetPlanes)
            .WithSummary("Obtiene planes paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<PlanListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("plan/search", SearchPlanes)
            .WithSummary("Busca planes por término de búsqueda")
            .Produces<ApiResponseDto<List<PlanSearchDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("plan/", CreatePlan)
            .WithName("CreatePlan")
            .WithSummary("Crea un nuevo plan")
            .Accepts<PlanEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("plan/{id}", UpdatePlan)
            .WithName("UpdatePlan")
            .WithSummary("Actualiza un plan")
            .Accepts<PlanEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("plan/{id}", DeletePlan)
            .WithName("DeletePlan")
            .WithSummary("Elimina un plan")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        // PlanHorario endpoints
        group.MapGet("plan/{planId:int}/horario/", GetPlanHorariosByPlanId)
            .WithName("GetPlanHorariosByPlanId")
            .WithSummary("Obtiene los horarios de un plan")
            .Produces<ApiResponseDto<List<PlanHorarioListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("plan/horario/{id:int}", GetPlanHorarioById)
            .WithName("GetPlanHorarioById")
            .WithSummary("Obtiene un horario de plan por ID")
            .Produces<ApiResponseDto<PlanHorarioEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("plan/{planId:int}/horario/", CreatePlanHorario)
            .WithName("CreatePlanHorario")
            .WithSummary("Crea un nuevo horario para un plan")
            .Accepts<PlanHorarioEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("plan/horario/{id:int}", UpdatePlanHorario)
            .WithName("UpdatePlanHorario")
            .WithSummary("Actualiza un horario de plan")
            .Accepts<PlanHorarioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("plan/horario/{id:int}", DeletePlanHorario)
            .WithName("DeletePlanHorario")
            .WithSummary("Elimina un horario de plan")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetPlanById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetPlanByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetPlanes(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(PlanListItemDto.NomPlan),
        [FromQuery] bool sortDescending = false,
        [FromQuery] int? periodicidadId = null,
        [FromQuery] int? servicioId = null
        )
    {
        var result = await queryMediator.QueryAsync(new GetPlanesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending,
            PeriodicidadId = periodicidadId,
            ServicioId = servicioId
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreatePlan(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PlanEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreatePlanCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdatePlan(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] PlanEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdatePlanCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeletePlan(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeletePlanCommand(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> SearchPlanes(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int maxResults = 10,
        [FromQuery] int? servicioId = null)
    {
        var result = await queryMediator.QueryAsync(new SearchPlanesQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults,
            ServicioId = servicioId
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetPlanHorariosByPlanId(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int planId)
    {
        var result = await queryMediator.QueryAsync(new GetPlanHorariosByPlanIdQuery { PlanId = planId });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetPlanHorarioById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetPlanHorarioByIdQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreatePlanHorario(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int planId,
        [FromBody] PlanHorarioEditDto model)
    {
        model.PlanId = planId;
        var result = await commandMediator.SendAsync(new CreatePlanHorarioCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdatePlanHorario(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] PlanHorarioEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdatePlanHorarioCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeletePlanHorario(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeletePlanHorarioCommand(id));
        return result.ToCustomMinimalApiResult();
    }
}