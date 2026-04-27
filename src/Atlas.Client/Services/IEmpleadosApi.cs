using Atlas.Shared.Common;
using Atlas.Shared.Empleados;
using Refit;

namespace Atlas.Client.Services;

public interface IEmpleadosApi
{
    [Get("/api/empleados/empleado")]
    Task<ApiResponseDto<PagedResultDto<EmpleadoListItemDto>>> GetEmpleadosAsync([Query] string? q = null, [Query] int page = 1, [Query] int size = 10, [Query] string? sortColumn = null, [Query] bool sortDescending = false);

    [Get("/api/empleados/empleado/{id}")]
    Task<ApiResponseDto<EmpleadoEditDto>> GetEmpleadoByIdAsync(int id);

    [Post("/api/empleados/empleado")]
    Task<ApiResponseDto<int>> CreateEmpleadoAsync([Body] EmpleadoEditDto model);

    [Put("/api/empleados/empleado/{id}")]
    Task<ApiResponseDto> UpdateEmpleadoAsync(int id, [Body] EmpleadoEditDto model);

    [Delete("/api/empleados/empleado/{id}")]
    Task<ApiResponseDto> DeleteEmpleadoAsync(int id);
}
