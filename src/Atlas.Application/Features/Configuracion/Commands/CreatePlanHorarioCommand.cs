using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Commands;

public record CreatePlanHorarioCommand(PlanHorarioEditDto Model) : ICommand<Result<int>>;

internal class CreatePlanHorarioCommandHandler(IAtlasDbContext context, IValidator<PlanHorarioEditDto> validator)
    : ICommandHandler<CreatePlanHorarioCommand, Result<int>>
{
    public async Task<Result<int>> HandleAsync(CreatePlanHorarioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validation = await validator.ValidateAsync(model, cancellationToken);
        if (!validation.IsValid)
            return Result.Invalid(validation.AsErrors());

        var horario = new PlanHorario
        {
            PlanId = model.PlanId,
            EmpleadoId = model.EmpleadoId,
            DiaSemana = model.DiaSemana,
            HoraInicio = model.HoraInicio,
            HoraFin = model.HoraFin,
            Activo = model.Activo
        };

        await context.PlanesHorario.AddAsync(horario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(horario.Id);
    }
}
