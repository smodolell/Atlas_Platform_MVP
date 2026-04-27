namespace Atlas.Shared.Servicios;

public class ServicioHorarioEditDto
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public int EmpleadoId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public bool Activo { get; set; } = true;
}

public class ServicioHorarioEditDtoValidator : AbstractValidator<ServicioHorarioEditDto>
{
    public ServicioHorarioEditDtoValidator()
    {
        RuleFor(x => x.EmpleadoId)
            .GreaterThan(0).WithMessage("Debe seleccionar un instructor.");

        RuleFor(x => x.HoraFin)
            .GreaterThan(x => x.HoraInicio).WithMessage("La hora de fin debe ser posterior a la hora de inicio.");
    }
}
