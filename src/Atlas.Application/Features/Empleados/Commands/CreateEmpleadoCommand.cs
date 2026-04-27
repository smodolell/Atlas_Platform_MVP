using Atlas.Domain.Entities;
using Atlas.Shared.Empleados;
using Atlas.Application.Common.Interfaces;
using Ardalis.Result;
using FluentValidation;

namespace Atlas.Application.Features.Empleados.Commands;

public record CreateEmpleadoCommand(EmpleadoEditDto Model) : ICommand<Result<int>>;

public class CreateEmpleadoCommandHandler(IAtlasDbContext context, IValidator<EmpleadoEditDto> validator) : ICommandHandler<CreateEmpleadoCommand, Result<int>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<EmpleadoEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateEmpleadoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var empleado = new Empleado
        {
            Nombre = model.Nombre,
            Apellido = model.Apellido
        };

        await _context.Empleados.AddAsync(empleado, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(empleado.Id);
    }
}
