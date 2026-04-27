using Atlas.Shared.Common;
using Atlas.Shared.Configuracion;
using Refit;

namespace Atlas.Client.Services;

public interface IConfiguracionApi
{
    [Get("/api/configuracion/plan/{id}")]
    Task<IApiResponse<ApiResponseDto<PlanEditDto>>> GetPlanByIdAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);

    [Get("/api/configuracion/plan/search")]
    Task<ApiResponseDto<List<PlanSearchDto>>> SearchPlanesAsync(
        [Query] string? searchTerm = null,
        [Query] int maxResults = 10,
        [Query] int? servicioId = null,
        CancellationToken cancellationToken = default);

    [Get("/api/configuracion/plan/")]
    Task<ApiResponseDto<PagedResultDto<PlanListItemDto>>> GetPlanesAsync(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "NomPlan",
        [Query] bool sortDescending = false,
        [Query] int? periodicidadId = null,
        [Query] int? servicioId = null,
        CancellationToken cancellationToken = default);

    [Post("/api/configuracion/plan/")]
    Task<IApiResponse<ApiResponseDto<Guid>>> CreatePlanAsync(
        [Body] PlanEditDto model,
        CancellationToken cancellationToken = default);

    [Put("/api/configuracion/plan/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdatePlanAsync(
        [AliasAs("id")] int id,
        [Body] PlanEditDto model,
        CancellationToken cancellationToken = default);

    [Delete("/api/configuracion/plan/{id}")]
    Task<IApiResponse<ApiResponseDto>> DeletePlanAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);

    [Get("/api/configuracion/plan/{planId}/horario/")]
    Task<ApiResponseDto<List<PlanHorarioListItemDto>>> GetPlanHorariosByPlanIdAsync(
        [AliasAs("planId")] int planId,
        CancellationToken cancellationToken = default);

    [Get("/api/configuracion/plan/horario/{id}")]
    Task<ApiResponseDto<PlanHorarioEditDto>> GetPlanHorarioByIdAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);

    [Post("/api/configuracion/plan/{planId}/horario/")]
    Task<ApiResponseDto<int>> CreatePlanHorarioAsync(
        [AliasAs("planId")] int planId,
        [Body] PlanHorarioEditDto model,
        CancellationToken cancellationToken = default);

    [Put("/api/configuracion/plan/horario/{id}")]
    Task<ApiResponseDto> UpdatePlanHorarioAsync(
        [AliasAs("id")] int id,
        [Body] PlanHorarioEditDto model,
        CancellationToken cancellationToken = default);

    [Delete("/api/configuracion/plan/horario/{id}")]
    Task<ApiResponseDto> DeletePlanHorarioAsync(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);
}