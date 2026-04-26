using FluentValidation;

namespace Atlas.Shared.Catalogos;

public class TipoPagoEditDto
{
    public string NomTipoPago { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}

public class TipoPagoEditDtoValidator : AbstractValidator<TipoPagoEditDto>
{
    public TipoPagoEditDtoValidator()
    {
        RuleFor(x => x.NomTipoPago)
            .NotEmpty().WithMessage("El nombre del tipo de pago es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");
    }
}
