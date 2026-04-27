using Atlas.Application.Common.Interfaces;
using Ardalis.Result;

namespace Atlas.Application.Features.Empleados.Commands;

public record DeleteEmpleadoCommand(int Id) : ICommand<Result>;

internal class DeleteEmpleadoCommandHandler(IAtlasDbContext context) : ICommandHandler<DeleteEmpleadoCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteEmpleadoCommand request, CancellationToken cancellationToken = default)
    {
        var empleado = await _context.Empleados.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (empleado == null)
            return Result.NotFound();

        _context.Empleados.Remove(empleado);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
