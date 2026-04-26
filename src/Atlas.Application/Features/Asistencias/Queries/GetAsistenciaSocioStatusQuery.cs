using Atlas.Domain.Enums;
using Atlas.Shared.Asistencias;

namespace Atlas.Application.Features.Asistencias.Queries;

public record GetAsistenciaSocioStatusQuery(Guid SocioId) : IQuery<Result<AsistenciaSocioStatusDto>>;

internal class GetAsistenciaSocioStatusQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetAsistenciaSocioStatusQuery, Result<AsistenciaSocioStatusDto>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<AsistenciaSocioStatusDto>> HandleAsync(GetAsistenciaSocioStatusQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.Plan)
                .Where(a => a.SocioId == request.SocioId && a.Estatus == EstatusAsistencia.EnCurso)
                .FirstOrDefaultAsync(cancellationToken);

            if (asistencia == null)
                return Result.Success(new AsistenciaSocioStatusDto { TieneEntradaAbierta = false });

            return Result.Success(new AsistenciaSocioStatusDto
            {
                TieneEntradaAbierta = true,
                AsistenciaId = asistencia.Id,
                FechaHoraEntrada = asistencia.FechaHoraEntrada,
                NomPlan = asistencia.Plan.NomPlan
            });
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
