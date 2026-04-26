using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Queries;

public record GetMembresiasPendientesBySocioQuery(Guid SocioId) : IQuery<Result<List<MembresiaConSaldoDto>>>;

internal class GetMembresiasPendientesBySocioQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetMembresiasPendientesBySocioQuery, Result<List<MembresiaConSaldoDto>>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<List<MembresiaConSaldoDto>>> HandleAsync(
        GetMembresiasPendientesBySocioQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var membresias = await _context.Membresias
                .Include(m => m.Plan)
                .Where(m => m.SocioId == request.SocioId && m.TotalSaldo > 0)
                .OrderBy(m => m.FechaVencimiento)
                .Select(m => new MembresiaConSaldoDto
                {
                    Id = m.Id,
                    NomPlan = m.Plan.NomPlan,
                    MontoSaldo = m.MontoSaldo,
                    IVASaldo = m.IVASaldo,
                    TotalSaldo = m.TotalSaldo,
                    FechaVencimiento = m.FechaVencimiento
                })
                .ToListAsync(cancellationToken);

            return Result.Success(membresias);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
