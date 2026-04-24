using Atlas.Shared.Accounts.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Atlas.Application.Features.Accounts.Usuarios.Queries;

public class GetUsuarioByIdQuery : IQuery<Result<UsuarioEditDto>>
{
    public required int UsuarioId { get; set; }
}

public class GetUsuarioByIdQueryHandler(
    UserManager<Usuario> userManager,
    IMapper mapper
) : IQueryHandler<GetUsuarioByIdQuery, Result<UsuarioEditDto>>
{
    private readonly UserManager<Usuario> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<UsuarioEditDto>> HandleAsync(GetUsuarioByIdQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oUsuario = await _userManager.FindByIdAsync(message.UsuarioId.ToString());
            if (oUsuario == null)
            {
                return Result.NotFound("Usuario no encontrado.");
            }

            var result = _mapper.Map<UsuarioEditDto>(oUsuario);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
