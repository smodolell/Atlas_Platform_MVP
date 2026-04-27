using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Commands;

public class UpdateServicioHorarioCommand : ICommand<Result>
{
    public int Id { get; init; }
    public ServicioHorarioEditDto Model { get; init; } = null!;
}

internal class UpdateServicioHorarioCommandHandler(IAtlasDbContext context, IValidator<ServicioHorarioEditDto> validator)
    : ICommandHandler<UpdateServicioHorarioCommand, Result>
{
    public async Task<Result> HandleAsync(UpdateServicioHorarioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validation = await validator.ValidateAsync(model, cancellationToken);
        if (!validation.IsValid)
            return Result.Invalid(validation.AsErrors());

        var horario = await context.ServicioHorarios.FindAsync([message.Id], cancellationToken: cancellationToken);
        if (horario == null)
            return Result.NotFound();

        horario.EmpleadoId = model.EmpleadoId;
        horario.DiaSemana = model.DiaSemana;
        horario.HoraInicio = model.HoraInicio;
        horario.HoraFin = model.HoraFin;
        horario.Activo = model.Activo;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
