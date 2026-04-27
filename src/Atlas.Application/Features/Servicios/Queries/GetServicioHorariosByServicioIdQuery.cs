using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Queries;

public class GetServicioHorariosByServicioIdQuery : IQuery<Result<List<ServicioHorarioListItemDto>>>
{
    public int ServicioId { get; init; }
}

internal class GetServicioHorariosByServicioIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetServicioHorariosByServicioIdQuery, Result<List<ServicioHorarioListItemDto>>>
{
    public async Task<Result<List<ServicioHorarioListItemDto>>> HandleAsync(GetServicioHorariosByServicioIdQuery request, CancellationToken cancellationToken = default)
    {
        var items = await context.ServicioHorarios
            .Where(h => h.ServicioId == request.ServicioId)
            .Select(h => new ServicioHorarioListItemDto
            {
                Id = h.Id,
                ServicioId = h.ServicioId,
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
