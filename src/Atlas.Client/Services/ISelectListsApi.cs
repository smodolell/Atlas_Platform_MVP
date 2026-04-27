using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;

public interface ISelectListsApi
{
    [Get("/api/select-lists/periodicidades")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetPeriodicidadSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    [Get("/api/select-lists/planes")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetPlanSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    [Get("/api/select-lists/tipos-pago")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTipoPagoSelectListAsync(
        CancellationToken cancellationToken = default);

    [Get("/api/select-lists/empleados")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetEmpleadoSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    [Get("/api/select-lists/servicios")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetServicioSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);
}