namespace Atlas.Application.Features.Asistencias.Specifications;

public class AsistenciasSpec : Specification<Asistencia>
{
    public AsistenciasSpec(string? searchText, DateTime? fecha = null, Guid? socioId = null)
    {
        if (socioId.HasValue)
        {
            Query.Where(a => a.SocioId == socioId.Value);
            if (fecha.HasValue)
                Query.Where(a => a.FechaHoraEntrada.Date == fecha.Value.Date);
        }
        else
        {
            var fechaFiltro = (fecha ?? DateTime.Today).Date;
            Query.Where(a => a.FechaHoraEntrada.Date == fechaFiltro);
        }

        if (!string.IsNullOrEmpty(searchText))
            Query.Where(a => a.Socio.Nombre.Contains(searchText)
                          || a.Socio.Apellido.Contains(searchText)
                          || a.Plan.NomPlan.Contains(searchText));

        Query.Include(a => a.Socio);
        Query.Include(a => a.Plan);
    }
}
