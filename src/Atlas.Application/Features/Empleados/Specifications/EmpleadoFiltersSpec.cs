using Atlas.Domain.Entities;
using Ardalis.Specification;

namespace Atlas.Application.Features.Empleados.Specifications;

public class EmpleadoFiltersSpec : Specification<Empleado>
{
    public EmpleadoFiltersSpec(string? searchText)
    {
        if(!string.IsNullOrEmpty(searchText))
        {
            var searchTerm = searchText.Trim().ToLower();
            Query.Where(e => e.Nombre.ToLower().Contains(searchTerm) || e.Apellido.ToLower().Contains(searchTerm));
        }
    }
}
