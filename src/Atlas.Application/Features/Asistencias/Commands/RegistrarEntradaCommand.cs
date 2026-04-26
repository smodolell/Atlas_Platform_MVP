using Atlas.Domain.Enums;
using Atlas.Shared.Asistencias;

namespace Atlas.Application.Features.Asistencias.Commands;

public record RegistrarEntradaCommand(RegistrarEntradaDto Model) : ICommand<Result<Guid>>;

internal class RegistrarEntradaCommandHandler(IAtlasDbContext context)
    : ICommandHandler<RegistrarEntradaCommand, Result<Guid>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<Guid>> HandleAsync(RegistrarEntradaCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var socioExiste = await _context.Socios.AnyAsync(s => s.Id == model.SocioId, cancellationToken);
        if (!socioExiste)
            return Result.NotFound("Socio no encontrado.");

        var tieneEntradaAbierta = await _context.Asistencias
            .AnyAsync(a => a.SocioId == model.SocioId && a.Estatus == EstatusAsistencia.EnCurso, cancellationToken);

        if (tieneEntradaAbierta)
            return Result.Invalid(new ValidationError("El socio ya tiene una entrada activa en este momento."));

        var membresia = await _context.Membresias
            .Where(m => m.SocioId == model.SocioId
                     && m.FechaInicio <= DateTime.Today
                     && m.FechaVencimiento >= DateTime.Today)
            .OrderByDescending(m => m.FechaVencimiento)
            .FirstOrDefaultAsync(cancellationToken);

        if (membresia == null)
            return Result.Invalid(new ValidationError("El socio no tiene una membresía activa vigente."));

        var asistencia = new Asistencia
        {
            Id = Guid.NewGuid(),
            SocioId = model.SocioId,
            PlanId = membresia.PlanId,
            Estatus = EstatusAsistencia.EnCurso,
            FechaHoraEntrada = DateTime.Now
        };

        await _context.Asistencias.AddAsync(asistencia, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(asistencia.Id);
    }
}
