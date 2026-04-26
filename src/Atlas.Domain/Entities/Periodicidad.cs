using Atlas.Domain.Enums;

namespace Atlas.Domain.Entities;

public class Periodicidad
{
    
    public int Id { get; set; }
    public string NomPeriodicidad { get; set; } = string.Empty;
    public UnidadTiempo Unidad { get; set; }
    public int Valor { get; set; }

    public bool Activa { get; set; }

    public ICollection<Plan> Planes { get; set; } = new HashSet<Plan>();
    

}
