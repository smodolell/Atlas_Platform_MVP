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

    [Get("/api/configuracion/plan/")]
    Task<ApiResponseDto<PagedResultDto<PlanListItemDto>>> GetPlanesAsync(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "NomPlan",
        [Query] bool sortDescending = false,
        [Query] int? periodicidadId = null ,
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
}