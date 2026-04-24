using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Commands;

public record CreateProductoCommand(ProductoEditDto Model) : ICommand<Result<int>>;

internal class CreateProductoCommandHandler(IAtlasDbContext context, IValidator<ProductoEditDto> validator) : ICommandHandler<CreateProductoCommand, Result<int>>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<ProductoEditDto> _validator = validator;
    public async Task<Result<int>> HandleAsync(CreateProductoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());
        var producto = new Producto
        {
            PeriodicidadId = model.PeriodicidadId,
            NomProducto = model.NomProducto,
            Descripcion = model.Descripcion,
            Precio = model.Precio,
            CupoMaximo = model.CupoMaximo
        };
        await _context.Productos.AddAsync(producto);
        await _context.SaveChangesAsync();
        return Result.Success(producto.Id);
    }
}