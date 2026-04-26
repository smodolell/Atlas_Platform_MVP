using Atlas.Shared.Catalogos;

namespace Atlas.Application.Features.Catalogos.Commands;

public record CreateTipoPagoCommand(TipoPagoEditDto Model) : ICommand<Result<int>>;

internal class CreateTipoPagoCommandHandler(IAtlasDbContext context, IValidator<TipoPagoEditDto> validator)
    : ICommandHandler<CreateTipoPagoCommand, Result<int>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<TipoPagoEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var existe = await _context.TiposPago
            .AnyAsync(t => t.NomTipoPago == model.NomTipoPago, cancellationToken);
        if (existe)
            return Result.Invalid(new ValidationError($"Ya existe un tipo de pago con el nombre '{model.NomTipoPago}'."));

        var tipoPago = new TipoPago
        {
            NomTipoPago = model.NomTipoPago,
            Activo = model.Activo
        };

        await _context.TiposPago.AddAsync(tipoPago, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(tipoPago.Id);
    }
}
