namespace Atlas.Shared.Configuracion;

public class PlanSearchDto
{
    public int Id { get; set; }
    public string NomPlan { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Activo { get; set; }
}
