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
        [FromQuery] int? periodicidadId = null
        )
    {
        var result = await queryMediator.QueryAsync(new GetPlanesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending,
            PeriodicidadId = periodicidadId
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
}