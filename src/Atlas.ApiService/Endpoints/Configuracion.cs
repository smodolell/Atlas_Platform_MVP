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

        group.MapGet("producto/{id}", GetProductoById)
            .WithName("GetProductoById")
            .WithSummary("Obtiene un producto por ID")
            .Produces<ApiResponseDto<ProductoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("producto/", GetProductos)
            .WithSummary("Obtiene productos paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<ProductoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("producto/", CreateProducto)
            .WithName("CreateProducto")
            .WithSummary("Crea un nuevo producto")
            .Accepts<ProductoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("producto/{id}", UpdateProducto)
            .WithName("UpdateProducto")
            .WithSummary("Actualiza un producto")
            .Accepts<ProductoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("producto/{id}", DeleteProducto)
            .WithName("DeleteProducto")
            .WithSummary("Elimina un producto")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetProductoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetProductoByIdQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetProductos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(ProductoListItemDto.NomProducto),
        [FromQuery] bool sortDescending = false,
        [FromQuery] int? periodicidadId = null
        )
    {
        var result = await queryMediator.QueryAsync(new GetProductosQuery
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

    public async Task<IResult> CreateProducto(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ProductoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateProductoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateProducto(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] ProductoEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateProductoCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteProducto(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteProductoCommand(id));
        return result.ToCustomMinimalApiResult();
    }
}