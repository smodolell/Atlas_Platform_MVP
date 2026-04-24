namespace Atlas.Domain.Entities;

public class AccessPointType
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string AccessPointTypeName { get; set; } = "";
    // Puede ser Page o ElementPage
}
