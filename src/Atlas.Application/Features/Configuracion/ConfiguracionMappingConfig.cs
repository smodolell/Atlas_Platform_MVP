using Atlas.Shared.Configuracion;

namespace Atlas.Application.Features.Configuracion;

public class ConfiguracionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Plan,PlanListItemDto>()
            .Map(o => o.NomPeriodicidad, d => d.Periodicidad.NomPeriodicidad);

    }
}
