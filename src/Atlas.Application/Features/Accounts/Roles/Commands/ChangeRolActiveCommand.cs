using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Roles.Commands;

public class ChangeRolActiveCommand : ICommand<Result>
{
    public required int RolId { get; set; }
    public required bool IsEnabled { get; set; }
}

public class ChangeRolActiveCommandHandler(
    RoleManager<Rol> roleManager
) : ICommandHandler<ChangeRolActiveCommand, Result>
{
    private readonly RoleManager<Rol> _roleManager = roleManager;

    public async Task<Result> HandleAsync(ChangeRolActiveCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oRol = await _roleManager.FindByIdAsync(message.RolId.ToString());
            if (oRol == null)
            {
                return Result.NotFound("ResponseMessages.RoleNotFound");
            }

            oRol.IsEnabled = message.IsEnabled;
            await _roleManager.UpdateAsync(oRol);
            return Result.SuccessWithMessage("");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
