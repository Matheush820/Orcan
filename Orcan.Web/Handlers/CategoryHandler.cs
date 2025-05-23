﻿using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Net.Http.Json;

namespace Orcan.Web.Handlers;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/categories", request);
        return await result.Content.ReadFromJsonAsync<Response<Category>>() 
            ?? new Response<Category>(null, 400, "falha ao criar categoria");
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        var result = await _client.DeleteAsync($"v1/categories/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Category>>()
            ?? new Response<Category>(null, 400, "falha ao excluir categoria");
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        var response = await _client.GetFromJsonAsync<PagedResponse<List<Category>>>($"v1/categories");

        return response ?? new PagedResponse<List<Category>>(null, 400, "Não foi possível obter as categorias");
    }


    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        var response = await _client.GetFromJsonAsync<Response<Category?>>($"v1/categories/{request.Id}");

        return response ?? new Response<Category?>(null, 400, "Não foi possível obter a categoria");
    }


    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Category>>()
            ?? new Response<Category>(null, 400, "falha ao atualizar categoria");
    }
}
