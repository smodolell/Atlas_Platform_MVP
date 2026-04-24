using Microsoft.AspNetCore.Identity;
namespace Atlas.Domain.Entities;

public class Usuario : IdentityUser<int>
{
    public DateTime FechaRegistro { get; set; }
    public string NombreCompleto { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Avatar { get; set; } = "";

}
