using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Commands;

public class UpdatePlanHorarioCommand : ICommand<Result>
{
    public int Id { get; init; }
    public PlanHorarioEditDto Model { get; init; } = null!;
}

internal class UpdatePlanHorarioCommandHandler(IAtlasDbContext context, IValidator<PlanHorarioEditDto> validator)
    : ICommandHandler<UpdatePlanHorarioCommand, Result>
{
    public async Task<Result> HandleAsync(UpdatePlanHorarioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validation = await validator.ValidateAsync(model, cancellationToken);
        if (!validation.IsValid)
            return Result.Invalid(validation.AsErrors());

        var horario = await context.PlanesHorario.FindAsync([message.Id], cancellationToken: cancellationToken);
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
