using System.Security.Claims;
using System.Text;

namespace Atlas.Client.Extensions;

public static class ClaimsPrincipalExtensions
{
    // Usamos ?.Value ?? "" para evitar el NullReference si el claim no existe
    public static string GetUserId(this ClaimsPrincipal user)
        => user.FindFirst("nameid")?.Value
           ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
           ?? string.Empty;

    public static string GetUserName(this ClaimsPrincipal user)
        => user.FindFirst("sub")?.Value
           ?? user.FindFirst(ClaimTypes.Name)?.Value
           ?? "Invitado";

    public static string GetUserEmail(this ClaimsPrincipal user)
        => user.FindFirst("email")?.Value
           ?? user.FindFirst(ClaimTypes.Email)?.Value
           ?? string.Empty;

    public static string GetFullName(this ClaimsPrincipal user)
        => user.FindFirst("unique_name")?.Value
           ?? user.GetUserName(); // Si no hay nombre completo, usamos el alias

    public static IEnumerable<string> GetUserRoles(this ClaimsPrincipal user)
        => user.FindAll("role").Select(r => r.Value)
           ?? user.FindAll(ClaimTypes.Role).Select(r => r.Value);

    public static string ToReadableString(this ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated != true)
            return "Usuario no autenticado";

        var sb = new StringBuilder();
        foreach (var claim in user.Claims)
        {
            sb.AppendLine($"{claim.Type}: {claim.Value}");
        }
        return sb.ToString();
    }
}