namespace Atlas.Application.Features.Configuracion.Commands;

public record DeletePlanCommand(int Id) : ICommand<Result>;

public class DeletePlanCommandHandler(IAtlasDbContext context) : ICommandHandler<DeletePlanCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(DeletePlanCommand message, CancellationToken cancellationToken = default)
    {
        // Buscar el producto por ID
        var plan = await _context.Planes
            .SingleOrDefaultAsync(p => p.Id == message.Id, cancellationToken);

        // Validar que el plan existe
        if (plan is null)
            return Result.Invalid(new ValidationError($"No se encontró un plan con el ID {message.Id}"));

        _context.Planes.Remove(plan);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}