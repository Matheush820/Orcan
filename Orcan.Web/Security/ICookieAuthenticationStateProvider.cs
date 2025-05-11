using Microsoft.AspNetCore.Components.Authorization;

namespace Orcan.Web.Security;

public interface ICookieAuthenticationStateProvider
{
    Task<bool> checkAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    void NotifyAuthenticationStateChanged();
}
