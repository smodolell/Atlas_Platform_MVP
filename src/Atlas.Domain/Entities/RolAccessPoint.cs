namespace Atlas.Domain.Entities;

public class RolAccessPoint
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public Guid AccessPointId { get; set; }


    [ForeignKey(nameof(RolId))]
    public Rol Rol { get; set; } = null!;

    [ForeignKey(nameof(AccessPointId))]
    public AccessPoint AccessPoint { get; set; } = null!;
}