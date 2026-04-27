using Atlas.Shared.Empleados;
using Atlas.Application.Common.Interfaces;
using Ardalis.Result;
using FluentValidation;

namespace Atlas.Application.Features.Empleados.Commands;

public class UpdateEmpleadoCommand : ICommand<Result>
{
    public int Id { get; init; }
    public EmpleadoEditDto Model { get; init; } = null!;
}

internal class UpdateEmpleadoCommandHandler(IAtlasDbContext context, IValidator<EmpleadoEditDto> validator) : ICommandHandler<UpdateEmpleadoCommand, Result>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<EmpleadoEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateEmpleadoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var empleado = await _context.Empleados.FindAsync([message.Id], cancellationToken: cancellationToken);
        if (empleado == null)
            return Result.NotFound();

        empleado.Nombre = model.Nombre;
        empleado.Apellido = model.Apellido;
        
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
