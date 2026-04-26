using Atlas.Shared.Catalogos;

namespace Atlas.Application.Features.Catalogos.Queries;

public record GetTipoPagoByIdQuery(int Id) : IQuery<Result<TipoPagoEditDto>>;

internal class GetTipoPagoByIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetTipoPagoByIdQuery, Result<TipoPagoEditDto>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<TipoPagoEditDto>> HandleAsync(GetTipoPagoByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var tipoPago = await _context.TiposPago
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (tipoPago is null)
                return Result.NotFound($"No se encontró un tipo de pago con el ID {request.Id}");

            return Result.Success(new TipoPagoEditDto
            {
                NomTipoPago = tipoPago.NomTipoPago,
                Activo = tipoPago.Activo
            });
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
