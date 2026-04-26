using Atlas.Shared.Asistencias;
using Atlas.Shared.Common;
using Refit;

namespace Atlas.Client.Services;

public interface IAsistenciasApi
{
    [Get("/api/asistencias/")]
    Task<ApiResponseDto<PagedResultDto<AsistenciaListItemDto>>> GetAsistenciasAsync(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 20,
        [Query] string sortColumn = "FechaHoraEntrada",
        [Query] bool sortDescending = true,
        [Query] DateTime? fecha = null,
        [Query] Guid? socioId = null,
        CancellationToken cancellationToken = default);

    [Get("/api/asistencias/socio-status/{socioId}")]
    Task<ApiResponseDto<AsistenciaSocioStatusDto>> GetAsistenciaSocioStatusAsync(
        Guid socioId,
        CancellationToken cancellationToken = default);

    [Post("/api/asistencias/entrada/")]
    Task<ApiResponseDto<Guid>> RegistrarEntradaAsync(
        [Body] RegistrarEntradaDto model,
        CancellationToken cancellationToken = default);

    [Put("/api/asistencias/salida/")]
    Task<ApiResponseDto> RegistrarSalidaAsync(
        [Body] RegistrarSalidaDto model,
        CancellationToken cancellationToken = default);
}
