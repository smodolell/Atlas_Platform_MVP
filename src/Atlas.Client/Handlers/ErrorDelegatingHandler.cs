using Atlas.Client.Exceptions;

namespace Atlas.Client.Handlers;

public class ErrorDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            throw new YggdrasilApiException(response.StatusCode, content);
        }

        return response;
    }
}