using Atlas.Application.Features.Auth.DTOs;

namespace Atlas.Application.Features.Auth.Commands;

public record LoginCommand : ICommand<Result<UsuarioLoginDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Contrasenia { get; set; } = string.Empty;
}
