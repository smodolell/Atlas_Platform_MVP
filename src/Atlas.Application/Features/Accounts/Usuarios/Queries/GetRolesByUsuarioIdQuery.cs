using Atlas.Shared.Accounts.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Usuarios.Queries;

public class GetRolesByUsuarioIdQuery : IQuery<Result<List<UsuarioRolDto>>>
{
    public required int UsuarioId { get; set; }
}

public class GetRolesByUsuarioIdQueryHandler(
    UserManager<Usuario> userManager,
    RoleManager<Rol> roleManager
) : IQueryHandler<GetRolesByUsuarioIdQuery, Result<List<UsuarioRolDto>>>
{
    private readonly UserManager<Usuario> _userManager = userManager;
    private readonly RoleManager<Rol> _roleManager = roleManager;
    public async Task<Result<List<UsuarioRolDto>>> HandleAsync(GetRolesByUsuarioIdQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oUsuario = await _userManager.FindByIdAsync(message.UsuarioId.ToString());
            if (oUsuario == null)
            {
                return Result.NotFound("Usuario no encontrado.");
            }

            var roles = await _roleManager.Roles
                .Where(r => r.NormalizedName != "WEBMASTER")
                .Select(s => new UsuarioRolDto
                {
                    Id = s.Id,
                    RolName = s.Name ?? "",
                    Selected = false
                })
                .ToListAsync(cancellationToken);

            foreach (var role in roles)
            {
                role.Selected = await _userManager.IsInRoleAsync(oUsuario, role.RolName);
            }

            return Result.Success(roles);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
