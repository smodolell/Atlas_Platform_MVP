using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;

public record GetPlanByIdQuery(int Id) : IQuery<Result<PlanEditDto>>;

internal class GetPlanByIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetPlanByIdQuery, Result<PlanEditDto>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<PlanEditDto>> HandleAsync(GetPlanByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var plan = await _context.Planes
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (plan is null)
                return Result.NotFound($"No se encontró un producto con el ID {request.Id}" );

            var dto = new PlanEditDto
            {
                NomPlan = plan.NomPlan,
                PeriodicidadId = plan.PeriodicidadId,
                Descripcion = plan.Descripcion,
                Precio = plan.Precio,
                CupoMaximo = plan.CupoMaximo,
                Activo = plan.Activo
            };

            return Result.Success(dto);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}