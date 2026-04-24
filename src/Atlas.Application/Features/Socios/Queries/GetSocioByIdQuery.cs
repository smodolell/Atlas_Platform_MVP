using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Queries;

public class GetSocioByIdQuery: IQuery<Result<SocioEditDto>>
{
    public Guid Id { get; init; }
}

internal class GetSocioByIdQueryHandler(IAtlasDbContext context) : IQueryHandler<GetSocioByIdQuery, Result<SocioEditDto>>
{
    private readonly IAtlasDbContext _context = context;
    public async Task<Result<SocioEditDto>> HandleAsync(GetSocioByIdQuery request, CancellationToken cancellationToken = default)
    {
        var socio = await _context.Socios.FindAsync(request.Id);
        if (socio == null)
            return Result.NotFound();

        var dto = new SocioEditDto
        {
            SocioId = socio.Id,
            Nombre = socio.Nombre,
            Apellido = socio.Apellido,
            DNI = socio.DNI,
            FechaNacimiento = socio.FechaNacimiento,
            Email = socio.Email,
            Telefono = socio.Telefono
        };
        return Result.Success(dto);
    }
}