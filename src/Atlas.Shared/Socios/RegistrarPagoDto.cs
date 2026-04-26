using FluentValidation;

namespace Atlas.Shared.Socios;

public class RegistrarPagoDto
{
    public int TipoPagoId { get; set; }
    public DateTime FechaPago { get; set; } = DateTime.Today;
    public List<Guid> MembresiaIds { get; set; } = [];
}

public class RegistrarPagoDtoValidator : AbstractValidator<RegistrarPagoDto>
{
    public RegistrarPagoDtoValidator()
    {
        RuleFor(x => x.TipoPagoId)
            .GreaterThan(0).WithMessage("Debe seleccionar un tipo de pago.");

        RuleFor(x => x.FechaPago)
            .NotEmpty().WithMessage("La fecha de pago es obligatoria.");

        RuleFor(x => x.MembresiaIds)
            .NotEmpty().WithMessage("Debe seleccionar al menos una membresía.");
    }
}
