using Atlas.Shared.Common;
using Atlas.Shared.Configuracion;
using Refit;

namespace Atlas.Client.Services;

public interface IConfiguracionApi
{
    [Get("/api/configuracion/producto/{id}")]
    Task<IApiResponse<ApiResponseDto<ProductoEditDto>>> GetProductoByIdAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);

    [Get("/api/configuracion/producto/")]
    Task<ApiResponseDto<PagedResultDto<ProductoListItemDto>>> GetProductosAsync(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "NomProducto",
        [Query] bool sortDescending = false,
        [Query] int? periodicidadId = null ,
        CancellationToken cancellationToken = default);

    [Post("/api/configuracion/producto/")]
    Task<IApiResponse<ApiResponseDto<Guid>>> CreateProductoAsync(
        [Body] ProductoEditDto model,
        CancellationToken cancellationToken = default);

    [Put("/api/configuracion/producto/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdateProductoAsync(
        [AliasAs("id")] int id,
        [Body] ProductoEditDto model,
        CancellationToken cancellationToken = default);

    [Delete("/api/configuracion/producto/{id}")]
    Task<IApiResponse<ApiResponseDto>> DeleteProductoAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);
}