using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.Socios.Commands;
using Atlas.Application.Features.Socios.Queries;
using Atlas.Shared.Common;
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

        group.MapGet("socio/{id}", GetSocioByIdAsync)
            .WithName("GetSocioById")
            .WithSummary("Obtiene un socio por ID")
            .Produces<ApiResponseDto<SocioEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("socio/", GetSociosAsync)
            .WithSummary("Obtiene socios paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<SocioListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("socio/search", SearchSociosAsync)
           .WithSummary("Busca socios por término de búsqueda")
           .Produces<ApiResponseDto<List<SocioSearchDto>>>(StatusCodes.Status200OK)
           .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
           .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("socio/", CreateSocioAsync)
            .WithName("CreateSocio")
            .WithSummary("Crea un nuevo socio")
            .Accepts<SocioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("socio/{id}", UpdateSocioAsync)
            .WithName("UpdateSocio")
            .WithSummary("Actualiza un socio")
            .Accepts<SocioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);


        group.MapDelete("socio/{id}", DeleteSocioAsync)
            .WithName("DeleteSocioAsync")
            .WithSummary("Elimina un socio")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);


        group.MapPost("membresia/", CreateMembresiaAsync)
            .WithName("CreateMembresiaAsync")
            .WithSummary("Crea una nueva membresía")
            .Accepts<CreateMembresiaDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

    }

    public async Task<IResult> GetSocioByIdAsync(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetSocioByIdQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }
    public async Task<IResult> SearchSociosAsync(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int maxResults = 10
    )
    {
        var result = await queryMediator.QueryAsync(new SearchSociosQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        });
        return result.ToCustomMinimalApiResult();
    }
    
    public async Task<IResult> GetSociosAsync(
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

    public async Task<IResult> CreateSocioAsync(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] SocioEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateSocioCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateSocioAsync(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] Guid id,
        [FromBody] SocioEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateSocioCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteSocioAsync(
      [FromServices] ICommandMediator commandMediator,
      [FromRoute] Guid id)
    {
        var result = await commandMediator.SendAsync(new DeleteSocioCommand(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateMembresiaAsync(
       [FromServices] ICommandMediator commandMediator,
       [FromBody] CreateMembresiaDto model)
    {
        var result = await commandMediator.SendAsync(new CreateMembresiaCommand(model));
        return result.ToCustomMinimalApiResult();
    }


}
