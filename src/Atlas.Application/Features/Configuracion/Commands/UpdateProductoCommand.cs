using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Commands;

public record UpdateProductoCommand : ICommand<Result>
{
    public int Id { get; init; }
    public ProductoEditDto Model { get; init; } = null!;
}

public class UpdateProductoCommandHandler(
    IAtlasDbContext context,
    IValidator<ProductoEditDto> validator) : ICommandHandler<UpdateProductoCommand, Result>
{
    private readonly IAtlasDbContext _context = context;
    private readonly IValidator<ProductoEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateProductoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var producto = await _context.Productos
            .FirstOrDefaultAsync(p => p.Id == message.Id, cancellationToken);

        if (producto is null)
            return Result.NotFound($"No se encontró un producto con el ID {message.Id}");

        var existeProductoConMismoNombre = await _context.Productos
            .AnyAsync(p => p.NomProducto == model.NomProducto && p.Id != message.Id, cancellationToken);

        if (existeProductoConMismoNombre)
            return Result.Invalid(new ValidationError($"Ya existe un producto con el nombre {model.NomProducto}"));

        producto.NomProducto = model.NomProducto;
        producto.Descripcion = model.Descripcion;
        producto.Precio = model.Precio;
        producto.CupoMaximo = model.CupoMaximo;
        producto.Activo = model.Activo;

        _context.Productos.Update(producto);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}