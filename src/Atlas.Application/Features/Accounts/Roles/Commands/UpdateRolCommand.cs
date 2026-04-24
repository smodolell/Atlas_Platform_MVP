using Atlas.Shared.Accounts.Roles;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Atlas.Application.Features.Accounts.Roles.Commands;

public class UpdateRolCommand : ICommand<Result>
{
    public required RolUpdateDto Model { get; set; }
}

internal class UpdateRolCommandHandler(
    RoleManager<Rol> roleManager,
    IValidator<RolUpdateDto> validator
) : ICommandHandler<UpdateRolCommand, Result>
{
    private readonly RoleManager<Rol> _roleManager = roleManager;
    private readonly IValidator<RolUpdateDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var oRol = await _roleManager.FindByIdAsync(model.RolId.ToString());
            if (oRol == null)
            {
                return Result.NotFound("ResponseMessages.RoleNotFound");
            }

            oRol.Name = model.Name;
            oRol.Descripcion = model.Descripcion;

            var result = await _roleManager.UpdateAsync(oRol);
            if (result.Succeeded)
            {
                return Result.SuccessWithMessage("ResponseMessages.RoleUpdatedSuccessfully");
            }

            var sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                sb.AppendLine($"{error.Code} - {error.Description}");
            }
            return Result.Error(sb.ToString());
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
