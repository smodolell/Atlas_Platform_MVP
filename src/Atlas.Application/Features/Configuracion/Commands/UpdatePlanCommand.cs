using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Commands;

public record UpdatePlanCommand : ICommand<Result>
{
    public int Id { get; init; }
    public PlanEditDto Model { get; init; } = null!;
}

public class UpdatePlanCommandHandler(
    IAtlasDbContext context,
    IValidator<PlanEditDto> validator) : ICommandHandler<UpdatePlanCommand, Result>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<PlanEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdatePlanCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var producto = await _context.Planes
            .FirstOrDefaultAsync(p => p.Id == message.Id, cancellationToken);

        if (producto is null)
            return Result.NotFound($"No se encontró un producto con el ID {message.Id}");

        var existePlanConMismoNombre = await _context.Planes
            .AnyAsync(p => p.NomPlan == model.NomPlan && p.Id != message.Id, cancellationToken);

        if (existePlanConMismoNombre)
            return Result.Invalid(new ValidationError($"Ya existe un producto con el nombre {model.NomPlan}"));
        producto.PeriodicidadId = model.PeriodicidadId;
        producto.NomPlan = model.NomPlan;
        producto.Descripcion = model.Descripcion;
        producto.Precio = model.Precio;
        producto.CupoMaximo = model.CupoMaximo;
        producto.Activo = model.Activo;

        _context.Planes.Update(producto);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}