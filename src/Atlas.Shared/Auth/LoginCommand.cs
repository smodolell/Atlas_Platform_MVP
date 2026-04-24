namespace Atlas.Shared.Auth;

public record LoginCommand 
{
    public string Email { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Contrasenia { get; set; } = string.Empty;
}
