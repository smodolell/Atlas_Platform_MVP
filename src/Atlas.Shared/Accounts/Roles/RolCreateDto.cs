using FluentValidation;

namespace Atlas.Shared.Accounts.Roles;

public class RolCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class RolCreateDtoValidator : AbstractValidator<RolCreateDto>
{
    public RolCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Nombre");

        RuleFor(x => x.Descripcion)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Descripción");
    }
}
