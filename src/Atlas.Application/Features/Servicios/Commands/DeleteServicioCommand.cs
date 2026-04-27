namespace Atlas.Application.Features.Servicios.Commands;

public record DeleteServicioCommand(int Id) : ICommand<Result>;

internal class DeleteServicioCommandHandler(IAtlasDbContext context)
    : ICommandHandler<DeleteServicioCommand, Result>
{
    public async Task<Result> HandleAsync(DeleteServicioCommand message, CancellationToken cancellationToken = default)
    {
        var servicio = await context.Servicios.FindAsync([message.Id], cancellationToken: cancellationToken);
        if (servicio == null)
            return Result.NotFound();

        context.Servicios.Remove(servicio);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
