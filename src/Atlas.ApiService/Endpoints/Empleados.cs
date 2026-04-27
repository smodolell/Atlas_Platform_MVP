using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.Empleados.Commands;
using Atlas.Application.Features.Empleados.Queries;
using Atlas.Shared.Common;
using Atlas.Shared.Empleados;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Empleados : EndpointGroupBase
{
    public override string? GroupName => "empleados";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
          .WithTags("Empleados");

        group.MapGet("empleado/{id:int}", GetEmpleadoByIdAsync)
            .WithName("GetEmpleadoById")
            .WithSummary("Obtiene un empleado por ID")
            .Produces<ApiResponseDto<EmpleadoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("empleado/", GetEmpleadosAsync)
            .WithSummary("Obtiene empleados paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<EmpleadoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("empleado/", CreateEmpleadoAsync)
            .WithName("CreateEmpleado")
            .WithSummary("Crea un nuevo empleado")
            .Accepts<EmpleadoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("empleado/{id:int}", UpdateEmpleadoAsync)
            .WithName("UpdateEmpleado")
            .WithSummary("Actualiza un empleado")
            .Accepts<EmpleadoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("empleado/{id:int}", DeleteEmpleadoAsync)
            .WithName("DeleteEmpleadoAsync")
            .WithSummary("Elimina un empleado")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetEmpleadoByIdAsync(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetEmpleadoByIdQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetEmpleadosAsync(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(EmpleadoListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetEmpleadosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateEmpleadoAsync(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] EmpleadoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateEmpleadoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateEmpleadoAsync(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] EmpleadoEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateEmpleadoCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteEmpleadoAsync(
      [FromServices] ICommandMediator commandMediator,
      [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteEmpleadoCommand(id));
        return result.ToCustomMinimalApiResult();
    }
}
