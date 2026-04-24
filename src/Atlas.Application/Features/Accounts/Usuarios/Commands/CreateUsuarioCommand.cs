using Atlas.Shared.Accounts.Usuarios;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Atlas.Application.Features.Accounts.Usuarios.Commands;

public class CreateUsuarioCommand : ICommand<Result>
{
    public required UsuarioCreateDto Model { get; set; }
}

public class CreateUsuarioCommandHandler(
    UserManager<Usuario> userManager,
    IValidator<UsuarioCreateDto> validator
) : ICommandHandler<CreateUsuarioCommand, Result>
{
    private readonly UserManager<Usuario> _userManager = userManager;
    private readonly IValidator<UsuarioCreateDto> _validator = validator;

    public async Task<Result> HandleAsync(CreateUsuarioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var oUsuario = new Usuario
            {
                UserName = model.UserName,
                Email = model.Email,
                NombreCompleto = model.NombreCompleto,
                Avatar = "",
                FechaRegistro = DateTime.Now,
                Telefono = model.Telefono,
            };

            var result = await _userManager.CreateAsync(oUsuario, model.Password);
            if (result.Succeeded)
            {
                return Result.SuccessWithMessage("Usuario creado exitosamente.");
            }

            return Result.Error(_getErrorsString(result));
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
