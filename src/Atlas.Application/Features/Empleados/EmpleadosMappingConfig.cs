using Atlas.Shared.Empleados;
using Mapster;

namespace Atlas.Application.Features.Empleados;

public class EmpleadosMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.Empleado, EmpleadoListItemDto>();
        config.NewConfig<Domain.Entities.Empleado, EmpleadoEditDto>();
    }
}
