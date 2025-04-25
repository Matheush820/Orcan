﻿using Orcan.Core.Handlers;
using Orcan.Core.Requests.Account;
using Orcan.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Orcan.Webbw.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
       var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

        return result.IsSuccessStatusCode
        ? new Response<string>("Login realizado com sucesso", 200, "Login realizado com sucesso")
        : new Response<string>(null, 400, "Não foi possivel realizar o login");

    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/register", request);

        return result.IsSuccessStatusCode
        ? new Response<string>("Cadasto realizado com sucesso", 201, "Cadasto realizado com sucesso")
        : new Response<string>(null, 400, "Não foi possivel realizar o Cadasto");
    }

    public async Task LogoutAsync()
    {
        var emptyContenr = new StringContent("{}", Encoding.UTF8, "application/json");
        await _client.PostAsJsonAsync("v1/identity/logout", emptyContenr);
    }
}