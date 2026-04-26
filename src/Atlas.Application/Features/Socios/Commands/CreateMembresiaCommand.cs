using Atlas.Domain.Enums;
using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios.Commands;

public record CreateMembresiaCommand(CreateMembresiaDto Model) : ICommand<Result>;

internal class CreateMembresiaCommandHandler(IAtlasDbContext context) : ICommandHandler<CreateMembresiaCommand, Result>
{
    private readonly IAtlasDbContext _context = context;

    public async Task<Result> HandleAsync(CreateMembresiaCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var socio = await _context.Socios
            .SingleOrDefaultAsync(s => s.Id == model.SocioId);
        if (socio == null) return Result.NotFound();

        var producto = await _context.Planes
            .SingleOrDefaultAsync(p => p.Id == model.ProductoId);
        if (producto == null) return Result.NotFound();

        var periodicidad = await _context.Periodicidades
            .SingleOrDefaultAsync(p => p.Id == producto.PeriodicidadId);

        if (periodicidad == null) return Result.NotFound();

        DateOnly fechaCurso = model.FechaInicio.HasValue ? DateOnly.FromDateTime(model.FechaInicio.Value) : DateOnly.FromDateTime(DateTime.UtcNow);
        for (int i = 0; i < model.Duracion; i++)
        {
            var membresia = new Membresia
            {
                Id = Guid.NewGuid(),
                SocioId = socio.Id,
                PlanId = model.ProductoId,
                FechaInicio = fechaCurso.ToDateTime(TimeOnly.MinValue),
                Monto = producto.Precio,
                IVA = producto.Precio * 0.21m,
            };
            var fechaFin = CalcularFechaFin(fechaCurso.ToDateTime(TimeOnly.MinValue), periodicidad, 1);

            membresia.Total = membresia.Monto + membresia.IVA;
            membresia.MontoSaldo = membresia.Monto;
            membresia.IVASaldo = membresia.IVA;
            membresia.TotalSaldo = membresia.Total;
            membresia.FechaFinalizacion = fechaFin;
            membresia.FechaVencimiento = fechaFin;

            if (producto.DiasGracia > 0)
            {
                membresia.DiasGracia = producto.DiasGracia;
                membresia.FechaVencimiento = fechaFin.AddDays(producto.DiasGracia);
            }
            switch (periodicidad.Unidad)
            {
                case UnidadTiempo.Dias:
                    fechaCurso = DateOnly.FromDateTime(fechaFin.AddDays(1));
                    break;
                default:
                    fechaCurso = DateOnly.FromDateTime(fechaFin);
                    break;
            }




            await _context.Membresias.AddAsync(membresia, cancellationToken);
        }


        await _context.SaveChangesAsync(cancellationToken);



        return Result.Success();
    }

    public DateTime CalcularFechaFin(DateTime inicio, Periodicidad p, int duracionContratada)
    {
        int totalAAgregar = p.Valor * duracionContratada;

        return p.Unidad switch
        {
            UnidadTiempo.Dias => inicio.AddDays(totalAAgregar),
            UnidadTiempo.Meses => inicio.AddMonths(totalAAgregar),
            UnidadTiempo.Anios => inicio.AddYears(totalAAgregar),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}