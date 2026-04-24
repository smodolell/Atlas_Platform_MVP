using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Commands;

public record CreateSocioCommand(SocioEditDto Model) : ICommand<Result<Guid>>;

public class CreateSocioCommandHandler(IAtlasDbContext context, IValidator<SocioEditDto> validator) : ICommandHandler<CreateSocioCommand, Result<Guid>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<SocioEditDto> _validator = validator;


    public async Task<Result<Guid>> HandleAsync(CreateSocioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        // Validar que no exista un socio con el mismo DNI
        var existsSocio = await _context.Socios
            .AnyAsync(s => s.DNI == model.DNI, cancellationToken);

        if (existsSocio)
            return Result.Invalid(new ValidationError($"Ya existe un socio con el DNI {model.DNI}" ));


        var socio = new Socio
        {
            Id = Guid.NewGuid(),
            DNI = model.DNI,
            FechaRegistro = DateTime.UtcNow,
            Nombre = model.Nombre,
            Apellido = model.Apellido,
            FechaNacimiento = model.FechaNacimiento!.Value,
            Email = model.Email,
            Telefono = model.Telefono,

        };

        await _context.Socios.AddAsync(socio);
        await _context.SaveChangesAsync();
        return Result.Success(socio.Id);

    }
}