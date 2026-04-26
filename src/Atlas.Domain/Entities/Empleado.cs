namespace Atlas.Domain.Entities;

public class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;

    public ICollection<PlanHorario> Horarios { get; set; } = new List<PlanHorario>();
    
}