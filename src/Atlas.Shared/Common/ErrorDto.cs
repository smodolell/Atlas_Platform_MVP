using System.Net;

namespace Atlas.Shared.Common;

public class ErrorDto
{
    public HttpStatusCode StatusCode { get; }
    public string Content { get; } = string.Empty;
    public Dictionary<string, List<string>> ValidationErrors { get; } = new();


}
