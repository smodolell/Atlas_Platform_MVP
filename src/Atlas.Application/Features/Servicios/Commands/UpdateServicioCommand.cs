using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Commands;

public class UpdateServicioCommand : ICommand<Result>
{
    public int Id { get; init; }
    public ServicioEditDto Model { get; init; } = null!;
}

internal class UpdateServicioCommandHandler(IAtlasDbContext context, IValidator<ServicioEditDto> validator)
    : ICommandHandler<UpdateServicioCommand, Result>
{
    public async Task<Result> HandleAsync(UpdateServicioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validation = await validator.ValidateAsync(model, cancellationToken);
        if (!validation.IsValid)
            return Result.Invalid(validation.AsErrors());

        var servicio = await context.Servicios.FindAsync([message.Id], cancellationToken: cancellationToken);
        if (servicio == null)
            return Result.NotFound();

        servicio.NomServicio = model.NomServicio;
        servicio.Descripcion = model.Descripcion;
        servicio.Activo = model.Activo;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
