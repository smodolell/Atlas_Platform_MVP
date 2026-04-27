using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Queries;

public class GetServicioByIdQuery : IQuery<Result<ServicioEditDto>>
{
    public int Id { get; init; }
}

internal class GetServicioByIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetServicioByIdQuery, Result<ServicioEditDto>>
{
    public async Task<Result<ServicioEditDto>> HandleAsync(GetServicioByIdQuery request, CancellationToken cancellationToken = default)
    {
        var servicio = await context.Servicios.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (servicio == null)
            return Result.NotFound();

        return Result.Success(new ServicioEditDto
        {
            Id = servicio.Id,
            NomServicio = servicio.NomServicio,
            Descripcion = servicio.Descripcion,
            Activo = servicio.Activo
        });
    }
}
