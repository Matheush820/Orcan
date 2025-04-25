using Orcan.Core.Requests.Account;
using Orcan.Core.Responses;

namespace Orcan.Core.Handlers;
public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();

}
