using Ardalis.Result;
using Atlas.Shared.Socios;
using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;

public interface ISociosApi
{
    [Get("/api/socios/socio/{id}")]
    Task<ApiResponseDto<SocioEditDto>> GetSocioById(Guid id);

    [Get("/api/socios/socio/")]
    Task<ApiResponseDto<PagedResultDto<SocioListItemDto>>> GetSocios(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/socios/socio/")]
    Task<ApiResponseDto> CreateSocio([Body] SocioEditDto model);

    [Put("/api/socios/socio/{id}")]
    Task<ApiResponseDto> UpdateSocio(Guid id, [Body] SocioEditDto model);

    [Delete("/api/socios/socio/{id}")]
    Task<ApiResponseDto> DeleteSocio(Guid id);
}
