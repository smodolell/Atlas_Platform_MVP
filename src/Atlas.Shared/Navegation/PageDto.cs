namespace Atlas.Shared.Navegation;

public class ApplicationDto
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
    public List<PageDto> Pages { get; set; } = new();
}
public class PageDto
{
    public string Menu { get; set; } = "";
    public string MenuIcon { get; set; } = "";
    public string MenuItem { get; set; } = "";
    public string Route { get; set; } = "";
    public bool IsAnonymous { get; set; }
    public AccessPointType AccessPointType { get; set; }
}
public enum AccessPointType
{
    LeftMenu,
    Page,
    Element
}
