using Atlas.Shared.Common;
using System.Net;
using System.Text.Json;

namespace Atlas.Client.Exceptions;

public class YggdrasilApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Content { get; }
    public Dictionary<string, List<string>> ValidationErrors { get; } = new();

    public YggdrasilApiException(HttpStatusCode statusCode, string content)
        : base($"API Error: {statusCode}")
    {
        StatusCode = statusCode;
        Content = content;

        try
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var apiResponse = JsonSerializer.Deserialize<ApiResponseDto<object>>(content, options);

            if (apiResponse?.Errors != null)
            {
                foreach (var error in apiResponse.Errors)
                {
                    var parts = error.Split(": ");
                    if (parts.Length > 1)
                    {
                        var key = parts[0].Trim();
                        var message = parts[1].Trim();

                        if (!ValidationErrors.ContainsKey(key))
                            ValidationErrors[key] = new List<string>();

                        ValidationErrors[key].Add(message);
                    }
                }
            }
        }
        catch { /* Fallback si el JSON no tiene el formato esperado */ }
    }
}

