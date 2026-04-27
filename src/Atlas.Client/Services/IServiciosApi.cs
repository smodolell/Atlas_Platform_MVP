using Atlas.Shared.Common;
using Atlas.Shared.Servicios;
using Refit;

namespace Atlas.Client.Services;

public interface IServiciosApi
{
    [Get("/api/servicios/servicio/search")]
    Task<ApiResponseDto<List<ServicioSearchDto>>> SearchServiciosAsync(
        [Query] string? searchTerm = null,
        [Query] int maxResults = 10,
        CancellationToken cancellationToken = default);

    [Get("/api/servicios/servicio")]
    Task<ApiResponseDto<PagedResultDto<ServicioListItemDto>>> GetServiciosAsync(
        [Query] string? q = null, [Query] int page = 1, [Query] int size = 10,
        [Query] string? sortColumn = null, [Query] bool sortDescending = false);

    [Get("/api/servicios/servicio/{id}")]
    Task<ApiResponseDto<ServicioEditDto>> GetServicioByIdAsync(int id);

    [Post("/api/servicios/servicio/")]
    Task<ApiResponseDto<int>> CreateServicioAsync([Body] ServicioEditDto model);

    [Put("/api/servicios/servicio/{id}")]
    Task<ApiResponseDto> UpdateServicioAsync(int id, [Body] ServicioEditDto model);

    [Delete("/api/servicios/servicio/{id}")]
    Task<ApiResponseDto> DeleteServicioAsync(int id);

    [Get("/api/servicios/servicio/{servicioId}/horario/")]
    Task<ApiResponseDto<List<ServicioHorarioListItemDto>>> GetHorariosAsync(int servicioId);

    [Post("/api/servicios/servicio/{servicioId}/horario/")]
    Task<ApiResponseDto<int>> CreateHorarioAsync(int servicioId, [Body] ServicioHorarioEditDto model);

    [Put("/api/servicios/servicio/horario/{id}")]
    Task<ApiResponseDto> UpdateHorarioAsync(int id, [Body] ServicioHorarioEditDto model);

    [Delete("/api/servicios/servicio/horario/{id}")]
    Task<ApiResponseDto> DeleteHorarioAsync(int id);
}
