using Atlas.Shared.Servicios;

namespace Atlas.Application.Features.Servicios.Commands;

public record CreateServicioHorarioCommand(ServicioHorarioEditDto Model) : ICommand<Result<int>>;

internal class CreateServicioHorarioCommandHandler(IAtlasDbContext context, IValidator<ServicioHorarioEditDto> validator)
    : ICommandHandler<CreateServicioHorarioCommand, Result<int>>
{
    public async Task<Result<int>> HandleAsync(CreateServicioHorarioCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validation = await validator.ValidateAsync(model, cancellationToken);
        if (!validation.IsValid)
            return Result.Invalid(validation.AsErrors());

        var horario = new ServicioHorario
        {
            ServicioId = model.ServicioId,
            EmpleadoId = model.EmpleadoId,
            DiaSemana = model.DiaSemana,
            HoraInicio = model.HoraInicio,
            HoraFin = model.HoraFin,
            Activo = model.Activo
        };

        await context.ServicioHorarios.AddAsync(horario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(horario.Id);
    }
}
