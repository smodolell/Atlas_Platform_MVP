namespace Atlas.Shared.Accounts.Roles;

public class MenuTreeItemDto
{
    public Guid Id { get; set; }
    public int MenuId { get; set; }
    public string? MenuIcon { get; set; }
    public string? MenuName { get; set; }
    public bool IsExpanded { get; set; }
    public bool IsChecked { get; set; }
    public List<MenuTreeItemDto> Childs { get; set; } = new();
}
