using FluentValidation;

namespace Atlas.Shared.Empleados;

public class EmpleadoEditDtoValidator : AbstractValidator<EmpleadoEditDto>
{
    public EmpleadoEditDtoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MaximumLength(150).WithMessage("El apellido no puede exceder los 150 caracteres.");
    }
}
