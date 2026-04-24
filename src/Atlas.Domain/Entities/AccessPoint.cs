namespace Atlas.Domain.Entities;

public class AccessPoint
{
    public Guid Id { get; set; }
    public int AccessPointTypeId { get; set; }
    public int MenuId { get; set; }
    public int ApplicationId { get; set; }

    public string AccessPointName { get; set; } = "";

    [MaxLength(500)]
    public string? Icon { get; set; }

    public string Route { get; set; } = "";

    public string? PageElementId { get; set; }
    public string DescPageElement { get; set; } = "";


    public int Order { get; set; }


    public bool IsAnonymous { get; set; }

    [ForeignKey(nameof(MenuId))]
    public Menu SYS_Menu { get; set; } = null!;


    [ForeignKey(nameof(AccessPointTypeId))]
    public AccessPointType SYS_AccessPointType { get; set; } = null!;
    public ICollection<RolAccessPoint> SYS_RolAccessPoint { get; set; } = new HashSet<RolAccessPoint>();
}
