using Atlas.Shared.Socios;
using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;

public interface ISociosApi
{
    [Get("/api/socios/socio/{id}")]
    Task<ApiResponseDto<SocioEditDto>> GetSocioById(Guid id);

    [Get("/api/socios/socio/")]
    Task<ApiResponseDto<PagedResultDto<SocioListItemDto>>> GetSociosAsync(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Get("/api/socios/search/")]
    Task<ApiResponseDto<List<SocioSearchDto>>> SearchSociosAsync(
     [Query] string? searchTerm = null,
     [Query] int maxResults = 10,
     CancellationToken cancellationToken = default);


    [Post("/api/socios/socio/")]
    Task<ApiResponseDto> CreateSocioAsync([Body] SocioEditDto model);

    [Put("/api/socios/socio/{id}")]
    Task<ApiResponseDto> UpdateSocio(Guid id, [Body] SocioEditDto model);

    [Delete("/api/socios/socio/{id}")]
    Task<ApiResponseDto> DeleteSocio(Guid id);


    [Post("/api/socios/membresia/")]
    Task<ApiResponseDto> CreateMembresiaAsync([Body] CreateMembresiaDto model);
}
