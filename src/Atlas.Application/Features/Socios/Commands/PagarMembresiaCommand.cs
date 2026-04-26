using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Commands;

public record PagarMembresiaCommand(RegistrarPagoDto Model) : ICommand<Result<Guid>>;

internal class PagarMembresiaCommandHandler(IAtlasDbContext context, IValidator<RegistrarPagoDto> validator)
    : ICommandHandler<PagarMembresiaCommand, Result<Guid>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<RegistrarPagoDto> _validator = validator;

    public async Task<Result<Guid>> HandleAsync(PagarMembresiaCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var membresias = await _context.Membresias
            .Where(m => model.MembresiaIds.Contains(m.Id) && m.TotalSaldo > 0)
            .ToListAsync(cancellationToken);

        if (membresias.Count == 0)
            return Result.Error("No se encontraron membresías con saldo pendiente.");

        if (membresias.Count != model.MembresiaIds.Count)
            return Result.Error("Algunas membresías no tienen saldo pendiente o no existen.");

        var pago = new Pago
        {
            Id = Guid.NewGuid(),
            TipoPagoId = model.TipoPagoId,
            FechaPago = model.FechaPago,
            MontoPago = membresias.Sum(m => m.TotalSaldo)
        };

        await _context.Pagos.AddAsync(pago, cancellationToken);

        foreach (var membresia in membresias)
        {
            var membresiaPago = new MembresiaPago
            {
                Id = Guid.NewGuid(),
                MembresiaId = membresia.Id,
                PagoId = pago.Id,
                Monto = membresia.MontoSaldo,
                IVA = membresia.IVASaldo,
                Total = membresia.TotalSaldo,
                FechaPago = model.FechaPago
            };

            await _context.MembresiaPagos.AddAsync(membresiaPago, cancellationToken);

            membresia.MontoSaldo = 0;
            membresia.IVASaldo = 0;
            membresia.TotalSaldo = 0;
            _context.Membresias.Update(membresia);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(pago.Id);
    }
}
