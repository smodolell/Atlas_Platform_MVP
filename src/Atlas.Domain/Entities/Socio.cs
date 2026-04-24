namespace Atlas.Domain.Entities;

public class Socio
{
    public Guid Id { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }

    public string? Email { get; set; } = string.Empty;
    public string? Telefono { get; set; } = string.Empty;


}

