namespace Atlas.Shared.Navegation;

public record AccessPointDto
{
    public string? MenuIcon { get; set; }

    public string? MenuName { get; set; }

    public string? Route { get; set; }

    public HashSet<AccessPointDto> Childs { get; set; } = new();
}
