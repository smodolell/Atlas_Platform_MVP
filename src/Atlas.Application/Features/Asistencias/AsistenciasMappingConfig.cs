using Atlas.Shared.Asistencias;

namespace Atlas.Application.Features.Asistencias;

public class AsistenciasMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Asistencia, AsistenciaListItemDto>()
            .Map(o => o.NomSocio, d => d.Socio.Nombre + " " + d.Socio.Apellido)
            .Map(o => o.NomPlan, d => d.Plan.NomPlan)
            .Map(o => o.Estatus, d => d.Estatus.ToString());
    }
}
