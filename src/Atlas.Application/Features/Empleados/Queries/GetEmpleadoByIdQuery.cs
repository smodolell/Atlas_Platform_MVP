using Atlas.Shared.Empleados;
using Atlas.Application.Common.Interfaces;
using Ardalis.Result;

namespace Atlas.Application.Features.Empleados.Queries;

public class GetEmpleadoByIdQuery : IQuery<Result<EmpleadoEditDto>>
{
    public int Id { get; init; }
}

internal class GetEmpleadoByIdQueryHandler(IAtlasDbContext context) : IQueryHandler<GetEmpleadoByIdQuery, Result<EmpleadoEditDto>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<EmpleadoEditDto>> HandleAsync(GetEmpleadoByIdQuery request, CancellationToken cancellationToken = default)
    {
        var empleado = await _context.Empleados.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (empleado == null)
            return Result.NotFound();

        var dto = new EmpleadoEditDto
        {
            Id = empleado.Id,
            Nombre = empleado.Nombre,
            Apellido = empleado.Apellido
        };
        return Result.Success(dto);
    }
}
