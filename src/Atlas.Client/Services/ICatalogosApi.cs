using Atlas.Shared.Catalogos;
using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;

public interface ICatalogosApi
{
    [Get("/api/catalogos/tipopago/{id}")]
    Task<IApiResponse<ApiResponseDto<TipoPagoEditDto>>> GetTipoPagoByIdAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);

    [Get("/api/catalogos/tipopago/")]
    Task<ApiResponseDto<PagedResultDto<TipoPagoListItemDto>>> GetTiposPagoAsync(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "NomTipoPago",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/catalogos/tipopago/")]
    Task<IApiResponse<ApiResponseDto<int>>> CreateTipoPagoAsync(
        [Body] TipoPagoEditDto model,
        CancellationToken cancellationToken = default);

    [Put("/api/catalogos/tipopago/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdateTipoPagoAsync(
        [AliasAs("id")] int id,
        [Body] TipoPagoEditDto model,
        CancellationToken cancellationToken = default);

    [Delete("/api/catalogos/tipopago/{id}")]
    Task<IApiResponse<ApiResponseDto>> DeleteTipoPagoAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);
}
