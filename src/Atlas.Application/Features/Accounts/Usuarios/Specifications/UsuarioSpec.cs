using Ardalis.Specification;
using LinqKit;
using System.Linq.Expressions;

namespace Atlas.Application.Features.Accounts.Usuarios.Specifications;

public sealed class UsuarioBySearchTextSpec : Specification<Usuario>
{
    public UsuarioBySearchTextSpec(string? searchText)
    {
        // Excluir al WEBMASTER siempre
        Query.Where(p => p.NormalizedUserName != "WEBMASTER");

        // Filtro por texto de búsqueda (si no está vacío)
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.UserName != null && p.UserName.Contains(searchText)
                          || p.NombreCompleto.Contains(searchText)
                          || p.Email != null && p.Email.Contains(searchText)
                          || p.Telefono != null && p.Telefono.Contains(searchText));
        }
    }
}
