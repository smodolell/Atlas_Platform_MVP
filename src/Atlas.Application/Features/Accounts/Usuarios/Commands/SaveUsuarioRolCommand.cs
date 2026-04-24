using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Atlas.Application.Features.Accounts.Usuarios.Commands;

public class SaveUsuarioRolCommand : ICommand<Result>
{
    public required int UsuarioId { get; set; }
    public required Dictionary<int, bool> Data { get; set; }
}

public class SaveUsuarioRolCommandHandler(
    UserManager<Usuario> userManager,
    RoleManager<Rol> roleManager
) : ICommandHandler<SaveUsuarioRolCommand, Result>
{
    private readonly UserManager<Usuario> _userManager = userManager;
    private readonly RoleManager<Rol> _roleManager = roleManager;

    public async Task<Result> HandleAsync(SaveUsuarioRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oUsuario = await _userManager.FindByIdAsync(message.UsuarioId.ToString());
            if (oUsuario == null)
            {
                return Result.NotFound("Usuario no encontrado.");
            }

            var sb = new StringBuilder();
            var error = false;
            var existingRoles = await _userManager.GetRolesAsync(oUsuario);

            foreach (var item in message.Data)
            {
                var oRol = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == item.Key, cancellationToken);
                if (oRol == null) continue;

                if (item.Value && !existingRoles.Any(a => a == oRol.Name))
                {
                    var addResult = await _userManager.AddToRoleAsync(oUsuario, oRol.Name ?? "");
                    if (!addResult.Succeeded)
                    {
                        sb.AppendLine(_getErrorsString(addResult));
                        error = true;
                    }
                }
                else if (!item.Value && existingRoles.Any(a => a == oRol.Name))
                {
                    var removeResult = await _userManager.RemoveFromRoleAsync(oUsuario, oRol.Name ?? "");
                    if (!removeResult.Succeeded)
                    {
                        sb.AppendLine(_getErrorsString(removeResult));
                        error = true;
                    }
                }
            }

            return error ? Result.Error(sb.ToString()) : Result.SuccessWithMessage("Roles actualizados exitosamente.");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    private static string _getErrorsString(IdentityResult result)
    {
        var sb = new StringBuilder();
        foreach (var error in result.Errors)
        {
            sb.AppendLine($"{error.Code} - {error.Description}");
        }
        return sb.ToString();
    }
}
