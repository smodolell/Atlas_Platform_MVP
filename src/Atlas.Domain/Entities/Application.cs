namespace Atlas.Domain.Entities;

public class Application
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string ApplicationName { get; set; } = "";

    public ICollection<AccessPoint> AccessPoints { get; set; } = new HashSet<AccessPoint>();

}
