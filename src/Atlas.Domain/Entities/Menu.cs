namespace Atlas.Domain.Entities;

public class Menu
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [MaxLength(1000)]
    public string? Icon { get; set; }

    [MaxLength(80)]
    public string Name { get; set; } = "";

    public int Order { get; set; }


    public ICollection<AccessPoint> AccessPoint { get; set; } = new HashSet<AccessPoint>();

}
