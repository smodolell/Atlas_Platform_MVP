namespace Atlas.Shared.Servicios;

public class ServicioEditDto
{
    public int Id { get; set; }
    public string NomServicio { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}

public class ServicioEditDtoValidator : AbstractValidator<ServicioEditDto>
{
    public ServicioEditDtoValidator()
    {
        RuleFor(x => x.NomServicio)
            .NotEmpty().WithMessage("El nombre del servicio es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.");
    }
}
