namespace Atlas.Application.Features.Configuracion.Commands;

public record DeletePlanHorarioCommand(int Id) : ICommand<Result>;

internal class DeletePlanHorarioCommandHandler(IAtlasDbContext context)
    : ICommandHandler<DeletePlanHorarioCommand, Result>
{
    public async Task<Result> HandleAsync(DeletePlanHorarioCommand message, CancellationToken cancellationToken = default)
    {
        var horario = await context.PlanesHorario.FindAsync([message.Id], cancellationToken: cancellationToken);
        if (horario == null)
            return Result.NotFound();

        context.PlanesHorario.Remove(horario);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
