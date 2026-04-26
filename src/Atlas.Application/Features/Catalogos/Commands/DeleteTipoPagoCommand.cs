namespace Atlas.Application.Features.Catalogos.Commands;

public record DeleteTipoPagoCommand(int Id) : ICommand<Result>;

internal class DeleteTipoPagoCommandHandler(IAtlasDbContext context)
    : ICommandHandler<DeleteTipoPagoCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        var tipoPago = await _context.TiposPago
            .SingleOrDefaultAsync(t => t.Id == message.Id, cancellationToken);

        if (tipoPago is null)
            return Result.NotFound($"No se encontró un tipo de pago con el ID {message.Id}");

        _context.TiposPago.Remove(tipoPago);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
