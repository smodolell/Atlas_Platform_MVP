namespace Atlas.Shared.Socios;

public class SocioEditDtoValidator : AbstractValidator<SocioEditDto>
{
    public SocioEditDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio.");
        RuleFor(x => x.DNI).NotEmpty().WithMessage("El DNI es obligatorio.");
        RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.");
    }
}