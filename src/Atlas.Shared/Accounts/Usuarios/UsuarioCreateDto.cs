using FluentValidation;
using System.Text.RegularExpressions;

namespace Atlas.Shared.Accounts.Usuarios;

public class UsuarioCreateDto
{
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
    public string NombreCompleto { get; set; } = "";
    public string Telefono { get; set; } = "";
}

public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .Length(4, 50).WithMessage("El nombre de usuario debe tener entre 4 y 50 caracteres")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("El nombre de usuario solo puede contener letras, números y guiones bajos");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("Debe ser una dirección de email válida")
            .MaximumLength(100).WithMessage("El email no puede exceder los 100 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres")
            .MaximumLength(100).WithMessage("La contraseña no puede exceder los 100 caracteres")
            .Must(p => p.Any(char.IsUpper)).WithMessage("La contraseña debe contener al menos una letra mayúscula")
            .Must(p => p.Any(char.IsLower)).WithMessage("La contraseña debe contener al menos una letra minúscula")
            .Must(p => p.Any(char.IsDigit)).WithMessage("La contraseña debe contener al menos un número")
            .Must(p => new Regex("[@$!%*?&]").IsMatch(p)).WithMessage("La contraseña debe contener al menos un carácter especial (@$!%*?&)");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es requerido")
            .Length(5, 100).WithMessage("El nombre completo debe tener entre 5 y 100 caracteres");

        RuleFor(x => x.Telefono)
            .NotEmpty();
    }
}
