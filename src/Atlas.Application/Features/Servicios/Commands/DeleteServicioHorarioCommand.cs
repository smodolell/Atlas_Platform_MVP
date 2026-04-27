namespace Atlas.Application.Features.Servicios.Commands;

public record DeleteServicioHorarioCommand(int Id) : ICommand<Result>;

internal class DeleteServicioHorarioCommandHandler(IAtlasDbContext context)
    : ICommandHandler<DeleteServicioHorarioCommand, Result>
{
    public async Task<Result> HandleAsync(DeleteServicioHorarioCommand message, CancellationToken cancellationToken = default)
    {
        var horario = await context.ServicioHorarios.FindAsync([message.Id], cancellationToken: cancellationToken);
        if (horario == null)
            return Result.NotFound();

        context.ServicioHorarios.Remove(horario);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
