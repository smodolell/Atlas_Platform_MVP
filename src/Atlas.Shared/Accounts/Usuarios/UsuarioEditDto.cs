using FluentValidation;

namespace Atlas.Shared.Accounts.Usuarios;

public class UsuarioEditDto
{
    public int UsuarioId { get; set; }
    public string Email { get; set; } = "";
    public string NombreCompleto { get; set; } = "";
    public string Telefono { get; set; } = "";
}

public class UsuarioEditDtoValidator : AbstractValidator<UsuarioEditDto>
{
    public UsuarioEditDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("Debe ser una dirección de email válida")
            .MaximumLength(100).WithMessage("El email no puede exceder los 100 caracteres");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es requerido")
            .Length(5, 100).WithMessage("El nombre completo debe tener entre 5 y 100 caracteres");

        RuleFor(x => x.Telefono).NotEmpty();
    }
}
