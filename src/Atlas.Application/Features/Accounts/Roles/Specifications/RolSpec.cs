namespace Atlas.Application.Features.Accounts.Roles.Specifications;

public sealed class RolSpec : Specification<Rol>
{
    public RolSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.Name != null && p.Name.Contains(searchText)
                          || p.Descripcion != null && p.Descripcion.Contains(searchText));
        }
    }
}