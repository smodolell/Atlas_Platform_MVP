using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.SelectLists.Queries;
using Atlas.Shared.Common;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class SelectLists : EndpointGroupBase
{
    public override string? GroupName => "select-lists";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Select Lists");

        group.MapGet("periodicidades", GetPeriodicidadSelectList)
            .WithName("GetPeriodicidadSelectList")
            .WithSummary("Obtiene Periodicidades")
            .WithDescription("Retorna una lista de los Periodicidades")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

       group.MapGet("planes", GetPlanSelectList)
            .WithName("GetPlanSelectList")
            .WithSummary("Obtiene Planes")
            .WithDescription("Retorna una lista de los Planes")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-pago", GetTipoPagoSelectList)
            .WithName("GetTipoPagoSelectList")
            .WithSummary("Obtiene Tipos de Pago activos")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("empleados", GetEmpleadoSelectList)
            .WithName("GetEmpleadoSelectList")
            .WithSummary("Obtiene Empleados para select")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("servicios", GetServicioSelectList)
            .WithName("GetServicioSelectList")
            .WithSummary("Obtiene Servicios activos para select")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }


    public async Task<IResult> GetPeriodicidadSelectList(
      [FromServices] IQueryMediator queryMediator,
      [FromQuery] string? searchTerm = null,
      [FromQuery] int? maxResults = null,
      CancellationToken cancellationToken = default)
    {
        var query = new GetPeriodicidadSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        };

        var result = await queryMediator.QueryAsync(query, cancellationToken);

        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetPlanSelectList(
  [FromServices] IQueryMediator queryMediator,
  [FromQuery] string? searchTerm = null,
  [FromQuery] int? maxResults = null,
  CancellationToken cancellationToken = default)
    {
        var query = new GetPlanSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        };

        var result = await queryMediator.QueryAsync(query, cancellationToken);

        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTipoPagoSelectList(
        [FromServices] IQueryMediator queryMediator,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagoSelectListQuery(), cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetEmpleadoSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetEmpleadoSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetServicioSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetServicioSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

}