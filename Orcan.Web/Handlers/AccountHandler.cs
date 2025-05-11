using Orcan.Core.Handlers;
using Orcan.Core.Requests.Account;
using Orcan.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Orcan.Web.Handlers
{
    public class AccountHandler : IACcountHandler
    {
        private readonly HttpClient _client;

        // Corrigido o nome do parâmetro e a sintaxe do construtor
        public AccountHandler(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        }

        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

            // Verificando o sucesso da requisição e retornando a resposta de forma adequada
            return result.IsSuccessStatusCode
                ? new Response<string>("Login realizado com sucesso", 200, "login efetuado")
                : new Response<string>(null, 400, "Não foi possível efetuar o login");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/register", request);

            // Verificando o sucesso da requisição e retornando a resposta de forma adequada
            return result.IsSuccessStatusCode
                ? new Response<string>("Cadastro realizado com sucesso", 200, "Cadastro efetuado")
                : new Response<string>(null, 400, "Não foi possível realizar o seu cadastro");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _client.PostAsync("v1/identity/logout", emptyContent);
        }
    }
}
