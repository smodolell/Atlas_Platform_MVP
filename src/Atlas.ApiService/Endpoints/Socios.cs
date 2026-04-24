using Atlas.ApiService.Infrastructure;
using Atlas.Application.Common.DTOs;
using Atlas.Application.Features.Socios.Commands;
using Atlas.Application.Features.Socios.Queries;
using Atlas.Shared.Socios;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Socios : EndpointGroupBase
{
    public override string? GroupName => "socios";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
          .WithTags("Socios");

        group.MapGet("socio/{id}", GetSocioById)
            .WithName("GetSocioById")
            .WithSummary("Obtiene un socio por ID")
            .Produces<ApiResponseDto<SocioEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("socio/", GetSocios)
            .WithSummary("Obtiene socios paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<SocioListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("socio/", CreateSocio)
            .WithName("CreateSocio")
            .WithSummary("Crea un nuevo socio")
            .Accepts<SocioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("socio/{id}", UpdateSocio)
            .WithName("UpdateSocio")
            .WithSummary("Actualiza un socio")
            .Accepts<SocioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);


        group.MapDelete("socio/{id}", DeleteSocio)
     .WithName("DeleteSocio")
     .WithSummary("Elimina un socio")
     .Produces(StatusCodes.Status200OK)
     .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
     .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
     .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetSocioById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetSocioByIdQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetSocios(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(SocioListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetSociosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateSocio(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] SocioEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateSocioCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateSocio(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] Guid id,
        [FromBody] SocioEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateSocioCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteSocio(
      [FromServices] ICommandMediator commandMediator,
      [FromRoute] Guid id)
    {
        var result = await commandMediator.SendAsync(new DeleteSocioCommand(id));
        return result.ToCustomMinimalApiResult();
    }
}
