using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Commands;

public record CreateServicioCommand(ServicioEditDto Model) : ICommand<Result<int>>;

internal class CreateServicioCommandHandler(IAtlasDbContext context, IValidator<ServicioEditDto> validator)
    : ICommandHandler<CreateServicioCommand, Result<int>>
{
    public async Task<Result<int>> HandleAsync(CreateServicioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validation = await validator.ValidateAsync(model, cancellationToken);
        if (!validation.IsValid)
            return Result.Invalid(validation.AsErrors());

        var servicio = new Servicio
        {
            NomServicio = model.NomServicio,
            Descripcion = model.Descripcion,
            Activo = model.Activo
        };

        await context.Servicios.AddAsync(servicio, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(servicio.Id);
    }
}
