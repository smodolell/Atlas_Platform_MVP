using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Commands;

public record CreatePlanCommand(PlanEditDto Model) : ICommand<Result<int>>;

internal class CreatePlanCommandHandler(IAtlasDbContext context, IValidator<PlanEditDto> validator) : ICommandHandler<CreatePlanCommand, Result<int>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<PlanEditDto> _validator = validator;
    public async Task<Result<int>> HandleAsync(CreatePlanCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());
        var plan = new Plan
        {
            PeriodicidadId = model.PeriodicidadId,
            NomPlan = model.NomPlan,
            Descripcion = model.Descripcion,
            Precio = model.Precio,
            CupoMaximo = model.CupoMaximo
        };
        await _context.Planes.AddAsync(plan);
        await _context.SaveChangesAsync();
        return Result.Success(plan.Id);
    }
}