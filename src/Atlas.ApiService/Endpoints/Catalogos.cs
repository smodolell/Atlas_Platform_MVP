using Atlas.ApiService.Infrastructure;
using Atlas.Application.Features.Catalogos.Commands;
using Atlas.Application.Features.Catalogos.Queries;
using Atlas.Shared.Catalogos;
using Atlas.Shared.Common;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Atlas.ApiService.Endpoints;

public class Catalogos : EndpointGroupBase
{
    public override string? GroupName => "catalogos";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Catálogos");

        group.MapGet("tipopago/{id}", GetTipoPagoById)
            .WithName("GetTipoPagoById")
            .WithSummary("Obtiene un tipo de pago por ID")
            .Produces<ApiResponseDto<TipoPagoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipopago/", GetTiposPago)
            .WithSummary("Obtiene tipos de pago paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<TipoPagoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tipopago/", CreateTipoPago)
            .WithName("CreateTipoPago")
            .WithSummary("Crea un nuevo tipo de pago")
            .Accepts<TipoPagoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("tipopago/{id}", UpdateTipoPago)
            .WithName("UpdateTipoPago")
            .WithSummary("Actualiza un tipo de pago")
            .Accepts<TipoPagoEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tipopago/{id}", DeleteTipoPago)
            .WithName("DeleteTipoPago")
            .WithSummary("Elimina un tipo de pago")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetTipoPagoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagoByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTiposPago(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TipoPagoListItemDto.NomTipoPago),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTiposPagoQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TipoPagoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTipoPagoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TipoPagoEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTipoPagoCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTipoPagoCommand(id));
        return result.ToCustomMinimalApiResult();
    }
}
