namespace Atlas.Shared.Socios;

public class SocioEditDto
{
    public Guid SocioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; } = DateTime.Now.AddYears(-18);

    public string? Email { get; set; } = string.Empty;
    public string? Telefono { get; set; } = string.Empty;
}
