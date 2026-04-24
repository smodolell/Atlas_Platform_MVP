using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion.Queries;

public class GetProductoByIdQuery : IQuery<Result<ProductoEditDto>>
{
    public int Id { get; init; }
}

internal class GetProductoByIdQueryHandler(IAtlasDbContext context)
    : IQueryHandler<GetProductoByIdQuery, Result<ProductoEditDto>>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result<ProductoEditDto>> HandleAsync(GetProductoByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (producto is null)
                return Result.NotFound($"No se encontró un producto con el ID {request.Id}" );

            var dto = new ProductoEditDto
            {
                NomProducto = producto.NomProducto,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                CupoMaximo = producto.CupoMaximo,
                Activo = producto.Activo
            };

            return Result<ProductoEditDto>.Success(dto);
        }
        catch (Exception ex)
        {
            return Result<ProductoEditDto>.Error(ex.Message);
        }
    }
}