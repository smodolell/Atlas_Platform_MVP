using Atlas.Shared.Accounts.Roles;
using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Roles.Queries;

public class GetRolByIdQuery : IQuery<Result<RolUpdateDto>>
{
    public required int RolId { get; set; }
}

public class GetRolByIdQueryHandler(
    RoleManager<Rol> roleManager
) : IQueryHandler<GetRolByIdQuery, Result<RolUpdateDto>>
{
    private readonly RoleManager<Rol> _roleManager = roleManager;

    public async Task<Result<RolUpdateDto>> HandleAsync(GetRolByIdQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oRol = await _roleManager.FindByIdAsync(message.RolId.ToString());
            if (oRol == null)
            {
                return Result.NotFound("ResponseMessages.RoleNotFound");
            }

            var result = new RolUpdateDto
            {
                RolId = oRol.Id,
                Name = oRol.Name ?? "",
                Descripcion = oRol.Descripcion ?? "",
            };

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
