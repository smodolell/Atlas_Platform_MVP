using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Commands;

public class UpdateSocioCommand : ICommand<Result>
{
    public Guid Id { get; init; }
    public SocioEditDto Model { get; init; } = null!;
}

internal class UpdateSocioCommandHandler(IAtlasDbContext context, IValidator<SocioEditDto> validator) : ICommandHandler<UpdateSocioCommand, Result>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<SocioEditDto> _validator = validator;
    public async Task<Result> HandleAsync(UpdateSocioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var socio = await _context.Socios.FindAsync(message.Id);
        if (socio == null)
            return Result.NotFound();

        socio.Nombre = model.Nombre;
        socio.Apellido = model.Apellido;
        socio.FechaNacimiento = model.FechaNacimiento!.Value;
        socio.Email = model.Email;
        socio.Telefono = model.Telefono;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}