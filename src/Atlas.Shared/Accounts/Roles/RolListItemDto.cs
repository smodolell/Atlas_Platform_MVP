namespace Atlas.Shared.Accounts.Roles;

public class RolListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}
