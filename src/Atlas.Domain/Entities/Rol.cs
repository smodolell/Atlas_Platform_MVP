using Microsoft.AspNetCore.Identity;

namespace Atlas.Domain.Entities;
public class Rol : IdentityRole<int>
{
    public string? Descripcion { get; set; }

    public bool IsEnabled { get; set; }

    public Rol(string rolName) : base(rolName)
    {
    }

    public Rol()
    {
    }


}

