namespace Atlas.Application.Features.Socios.Commands;

public record DeleteSocioCommand(Guid Id) : ICommand<Result>;

public class DeleteSocioCommandHandler(IAtlasDbContext context) : ICommandHandler<DeleteSocioCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteSocioCommand message, CancellationToken cancellationToken = default)
    {
        // Buscar el socio por ID
        var socio = await _context.Socios
            .FirstOrDefaultAsync(s => s.Id == message.Id, cancellationToken);

        // Validar que el socio existe
        if (socio is null)
            return Result.NotFound($"No se encontró un socio con el ID {message.Id}" );

        // Eliminar el socio
        _context.Socios.Remove(socio);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}