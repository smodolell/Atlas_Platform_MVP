



using Atlas.Shared.Socios;

namespace Atlas.Application.Features.Socios;

public class ProductsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Membresia, MembresiaListItemDto>()
            .Map(o => o.NomPlan, d => d.Plan.NomPlan);

    }
}
