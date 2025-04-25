using Microsoft.AspNetCore.Components.Authorization;
using Orcan.Core.Models.Account;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Orcan.Webbw.Security;

public class CookierAuthenticationStateProvider(IHttpClientFactory clientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    private bool _isAuthenticated = false;

    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        var userInfo = await GetUser();

        if (userInfo is null)
            return new AuthenticationState(user);

        var claims = await GetClaims(userInfo);

        var id = new ClaimsIdentity(claims, nameof(CookierAuthenticationStateProvider));
        user = new ClaimsPrincipal(id);

        _isAuthenticated = true;
        return new AuthenticationState(user);
    }

    private async Task<User?> GetUser()
    {
        try
        {
            return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");

        }
        catch
        {
            return null;
        }
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email),
        };

        claims.AddRange(user.Claims.Where(x => x.Key != ClaimTypes.Name &&
        x.Key != ClaimTypes.Email).Select(x => new Claim(x.Key, x.Value)));

        RoleClaim[]? roles;
        try
        {
            roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/manage/roles");
        }
        catch
        {
            return claims;
        }

       foreach(var role in roles ?? [])
        {
            if(string.IsNullOrEmpty(role.Type) && string.IsNullOrEmpty(role.Value))
                claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
        }

    return claims;
    }


    public void NotifyAuthenticationStateChanged()
    {
        // Fix: Ensure the method is invoked and the result is passed as a Task<AuthenticationState>
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
