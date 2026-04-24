namespace Atlas.Application.Features.Configuracion.Commands;

public record DeleteProductoCommand(int Id) : ICommand<Result>;

public class DeleteProductoCommandHandler(IAtlasDbContext context) : ICommandHandler<DeleteProductoCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteProductoCommand message, CancellationToken cancellationToken = default)
    {
        // Buscar el producto por ID
        var producto = await _context.Productos
            .SingleOrDefaultAsync(p => p.Id == message.Id, cancellationToken);

        // Validar que el producto existe
        if (producto is null)
            return Result.Invalid(new ValidationError($"No se encontró un producto con el ID {message.Id}"));

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}