namespace Atlas.Shared.Configuracion;

public class ProductoEditDto
{
    public int ProductoId { get; set; }
    public string NomProducto { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int CupoMaximo { get; set; }

    public bool Activo { get; set; }
}

public class ProductoEditDtoValidator : AbstractValidator<ProductoEditDto>
{
    public ProductoEditDtoValidator()
    {
        RuleFor(x => x.NomProducto)
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