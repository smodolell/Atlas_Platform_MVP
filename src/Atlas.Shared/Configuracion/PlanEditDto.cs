namespace Atlas.Shared.Configuracion;

public class PlanEditDto
{
    public int ProductoId { get; set; }
    public int ServicioId { get; set; }
    public int PeriodicidadId { get; set; }
    public string NomPlan { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int CupoMaximo { get; set; }

    public bool EsLibre { get; set; }
    public bool EsProgramado { get; set; }
    public bool EsTicket { get; set; }

    public bool Activo { get; set; }
}

public class PlanEditDtoValidator : AbstractValidator<PlanEditDto>
{
    public PlanEditDtoValidator()
    {
        RuleFor(x => x.ServicioId)
            .GreaterThan(0).WithMessage("Debe seleccionar un servicio.");
        RuleFor(x => x.NomPlan)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre del producto no puede exceder los 100 caracteres.");
        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción del producto no puede exceder los 500 caracteres.");
        RuleFor(x => x.Precio)
            .GreaterThanOrEqualTo(0).WithMessage("El precio del producto debe ser mayor o igual a cero.");
        RuleFor(x => x.CupoMaximo)
            .GreaterThanOrEqualTo(0).WithMessage("El cupo máximo del producto debe ser mayor o igual a cero.");
    }
}