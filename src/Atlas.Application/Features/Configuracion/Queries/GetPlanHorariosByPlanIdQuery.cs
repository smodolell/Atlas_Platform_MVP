using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;

public class GetPlanHorariosByPlanIdQuery : IQuery<Result<List<PlanHorarioListItemDto>>>
{
    public int PlanId { get; init; }
}

internal class GetPlanHorariosByPlanIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetPlanHorariosByPlanIdQuery, Result<List<PlanHorarioListItemDto>>>
{
    public async Task<Result<List<PlanHorarioListItemDto>>> HandleAsync(GetPlanHorariosByPlanIdQuery request, CancellationToken cancellationToken = default)
    {
        var items = await context.PlanesHorario
            .Where(h => h.PlanId == request.PlanId)
            .Select(h => new PlanHorarioListItemDto
            {
                Id = h.Id,
                PlanId = h.PlanId,
                EmpleadoId = h.EmpleadoId,
                NomEmpleado = h.Empleado.Nombre + " " + h.Empleado.Apellido,
                DiaSemana = h.DiaSemana,
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin,
                Activo = h.Activo
            })
            .OrderBy(h => h.DiaSemana)
            .ThenBy(h => h.HoraInicio)
            .ToListAsync(cancellationToken);

        return Result.Success(items);
    }
}
