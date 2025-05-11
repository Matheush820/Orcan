using Orcan.Core.Common.Extension;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Transactions;
using Orcan.Core.Responses;
using System.Net.Http;
using System.Net.Http.Json;

namespace Orcan.Web.Handlers;

public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/transactions", request);
        var response = await result.Content.ReadFromJsonAsync<Response<Transaction?>>();
        return response ?? new Response<Transaction?>(null, 400, "falha ao criar transação");
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        var result = await _client.DeleteAsync($"v1/transactions/{request.Id}");
        var response = await result.Content.ReadFromJsonAsync<Response<Transaction?>>();
        return response ?? new Response<Transaction?>(null, 400, "falha ao editar transação");
    }

    public async Task<Response<Transaction?>> GetByIdRequest(GetTransactionByIdRequest request)
    {
        return await _client.GetFromJsonAsync<Response<Transaction?>>($"v1/transactions/{request.Id}")
            ?? new Response<Transaction?>(null, 400, "Não foi possivel obter a transação"); 
    }

    public async Task<PagedResponse<List<Transaction>>> GetByPeriod(GetTransactionsByPeriodRequest request)
    {
        const string format = "yyyy-MM-dd";
        var startDate = request.StartDate?.ToString(format) ?? DateTime.Now.GetFirstDay().ToString(format);
        var endDate = request.EndDate?.ToString(format) ?? DateTime.Now.GetLastDay().ToString(format);

        var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";

        var response = await _client.GetFromJsonAsync<PagedResponse<List<Transaction>>>(url);

        return response ?? new PagedResponse<List<Transaction>>(null, 400, "Não foi possível obter as transações");
    }


    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
        var response = await result.Content.ReadFromJsonAsync<Response<Transaction?>>();
        return response ?? new Response<Transaction?>(null, 400, "falha ao editar transação");
    }
}
