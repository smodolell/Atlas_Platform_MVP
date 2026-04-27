using Atlas.Shared.Servicios;
using Mapster;

namespace Atlas.Application.Features.Servicios;

public class ServiciosMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Servicio, ServicioListItemDto>();
        config.NewConfig<Servicio, ServicioEditDto>();
    }
}
