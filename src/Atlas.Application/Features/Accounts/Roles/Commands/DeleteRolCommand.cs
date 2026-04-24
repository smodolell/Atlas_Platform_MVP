using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Roles.Commands;

public class DeleteRolCommand : ICommand<Result>
{
    public required int RolId { get; set; }
}

public class DeleteRolCommandHandler(
    RoleManager<Rol> roleManager
) : ICommandHandler<DeleteRolCommand, Result>
{
    private readonly RoleManager<Rol> _roleManager = roleManager;

    public async Task<Result> HandleAsync(DeleteRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oRol = await _roleManager.FindByIdAsync(message.RolId.ToString());
            if (oRol == null)
            {
                return Result.NotFound("Role not found");
            }

            await _roleManager.DeleteAsync(oRol);
            return Result.SuccessWithMessage("Role deleted successfully");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
