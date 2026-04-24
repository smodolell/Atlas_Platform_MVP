namespace Atlas.Shared.Accounts.Usuarios;

public class UserChangePasswordDto
{
    public int UserId { get; set; }
    public string NewPassword { get; set; } = "";
    public string ConfirmNewPassword { get; set; } = "";
}

public class UserChangePasswordDtoValidator : AbstractValidator<UserChangePasswordDto>
{
    public UserChangePasswordDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("El ID de usuario es requerido.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("La nueva contraseña es requerida.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("La confirmación de la contraseña es requerida.")
            .Equal(x => x.NewPassword).WithMessage("La nueva contraseña y la confirmación no coinciden.");
    }
}
