using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace Atlas.Client.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime js) => _js = js;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(_anonymous);
        var claims = ParseClaimsFromJwt(token);
        var expiry = claims.FirstOrDefault(c => c.Type == "exp")?.Value;

        if (expiry != null)
        {
            var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry));
            if (datetime.UtcDateTime <= DateTime.UtcNow)
            {
                // Token expirado: Limpiamos y devolvemos anónimo
                await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
                return new AuthenticationState(_anonymous);
            }
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    // Método para notificar a la app cuando el usuario hace Login/Logout
    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(_anonymous));
        NotifyAuthenticationStateChanged(authState);
    }

    //private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    //{
    //    var payload = jwt.Split('.')[1];
    //    var jsonBytes = ParseBase64WithoutPadding(payload);
    //    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
    //    return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    //}
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = new List<Claim>();
        foreach (var kvp in keyValuePairs!)
        {
            // Mapeo manual para asegurar compatibilidad con .NET
            var type = kvp.Key switch
            {
                "nameid" => ClaimTypes.NameIdentifier,
                "email" => ClaimTypes.Email,
                "unique_name" => ClaimTypes.Name,
                _ => kvp.Key
            };
            claims.Add(new Claim(type, kvp.Value.ToString()!));
        }
        return claims;
    }
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
