using Atlas.Application.Features.Auth.DTOs;

namespace Atlas.Application.Features.Auth.Interfaces;

public interface IJwtService
{
    string GenerateToken(UsuarioLoginDto user);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
    UsuarioLoginDto? GetUserFromToken(string token);
}
