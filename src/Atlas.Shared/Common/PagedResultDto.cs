namespace Atlas.Shared.Common;

public class PagedResultDto<T>
      where T : class, new()
{
    public List<T> Results { get; set; }  = new List<T>();
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}