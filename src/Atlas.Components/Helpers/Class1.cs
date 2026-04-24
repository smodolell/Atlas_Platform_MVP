using Atlas.Components.Attributes;
using Atlas.Shared.Navegation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Atlas.Components.Helpers;

public static class Utils
{
    public static List<PageDto> GetPagesFromAssembly(Assembly assembly)
    {
        var components = assembly.ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

        return components
           .Select(GetRouteFromComponent)
           .Where(page => page is not null)
           .Select(page => page!)
           .ToList();
    }

    private static PageDto? GetRouteFromComponent(Type component)
    {
        var attributes = component.GetCustomAttributes(inherit: true);

        var routeAttr = attributes.OfType<RouteAttribute>().FirstOrDefault();
        var accessAttr = attributes.OfType<AccessPointAttribute>().FirstOrDefault();

        // Si no es una página ruteable con nuestro atributo, ignorar
        if (routeAttr is null || accessAttr is null) return null;

        var route = routeAttr.Template;

        if (string.IsNullOrEmpty(route)) return null;

        // Limpieza de parámetros de ruta {id?} para que coincida con la base de datos
        if (route.Contains('{'))
        {
            route = route.Split('{')[0].TrimEnd('/');
        }

        return new PageDto
        {
            Menu = accessAttr.Menu,
            MenuIcon = accessAttr.MenuIcon,
            MenuItem = accessAttr.ItemMenu,
            Route = route,
            AccessPointType = accessAttr.AccessPointType,
            IsAnonymous = attributes.OfType<AllowAnonymousAttribute>().Any()
        };
    }
}