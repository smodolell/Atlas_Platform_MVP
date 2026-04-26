using Atlas.Shared.Catalogos;

namespace Atlas.Application.Features.Catalogos.Commands;

public record UpdateTipoPagoCommand : ICommand<Result>
{
    public int Id { get; init; }
    public TipoPagoEditDto Model { get; init; } = null!;
}

internal class UpdateTipoPagoCommandHandler(IAtlasDbContext context, IValidator<TipoPagoEditDto> validator)
    : ICommandHandler<UpdateTipoPagoCommand, Result>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<TipoPagoEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var tipoPago = await _context.TiposPago
            .FirstOrDefaultAsync(t => t.Id == message.Id, cancellationToken);

        if (tipoPago is null)
            return Result.NotFound($"No se encontró un tipo de pago con el ID {message.Id}");

        var existe = await _context.TiposPago
            .AnyAsync(t => t.NomTipoPago == model.NomTipoPago && t.Id != message.Id, cancellationToken);
        if (existe)
            return Result.Invalid(new ValidationError($"Ya existe un tipo de pago con el nombre '{model.NomTipoPago}'."));

        tipoPago.NomTipoPago = model.NomTipoPago;
        tipoPago.Activo = model.Activo;

        _context.TiposPago.Update(tipoPago);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
