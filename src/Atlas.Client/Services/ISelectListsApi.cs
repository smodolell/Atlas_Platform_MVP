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
}