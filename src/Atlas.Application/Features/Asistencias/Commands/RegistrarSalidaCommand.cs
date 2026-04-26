using Atlas.Domain.Enums;
using Atlas.Shared.Asistencias;

namespace Atlas.Application.Features.Asistencias.Commands;

public record RegistrarSalidaCommand(RegistrarSalidaDto Model) : ICommand<Result>;

internal class RegistrarSalidaCommandHandler(IAtlasDbContext context)
    : ICommandHandler<RegistrarSalidaCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(RegistrarSalidaCommand message, CancellationToken cancellationToken = default)
    {
        var asistencia = await _context.Asistencias
            .FirstOrDefaultAsync(a => a.Id == message.Model.AsistenciaId, cancellationToken);

        if (asistencia == null)
            return Result.NotFound("Asistencia no encontrada.");

        if (asistencia.Estatus != EstatusAsistencia.EnCurso)
            return Result.Invalid(new ValidationError("La asistencia ya fue completada o cancelada."));

        asistencia.FechaHoraSalida = DateTime.Now;
        asistencia.Estatus = EstatusAsistencia.Completada;

        _context.Asistencias.Update(asistencia);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
