using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;

public class GetPlanHorarioByIdQuery : IQuery<Result<PlanHorarioEditDto>>
{
    public int Id { get; init; }
}

internal class GetPlanHorarioByIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetPlanHorarioByIdQuery, Result<PlanHorarioEditDto>>
{
    public async Task<Result<PlanHorarioEditDto>> HandleAsync(GetPlanHorarioByIdQuery request, CancellationToken cancellationToken = default)
    {
        var horario = await context.PlanesHorario.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (horario == null)
            return Result.NotFound();

        return Result.Success(new PlanHorarioEditDto
        {
            Id = horario.Id,
            PlanId = horario.PlanId,
            EmpleadoId = horario.EmpleadoId,
            DiaSemana = horario.DiaSemana,
            HoraInicio = horario.HoraInicio,
            HoraFin = horario.HoraFin,
            Activo = horario.Activo
        });
    }
}
