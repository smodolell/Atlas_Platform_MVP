using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios;

public class SociosMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Membresia, MembresiaListItemDto>()
            .Map(o => o.NomPlan, d => d.Plan.NomPlan)
            .Map(o => o.NomSocio, d => d.Socio.Nombre + " " + d.Socio.Apellido);

    }
}
