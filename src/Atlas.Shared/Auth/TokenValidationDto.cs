namespace Atlas.Shared.Auth;

public class TokenValidationDto
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public UsuarioLoginDto? User { get; set; }
}