using Microsoft.AspNetCore.Mvc;
using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories.Herança;
using Orçan.Core.Responses;

namespace Orçan.Api.Common.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
 => app.MapGet("/", HandleAsync)
     .WithName("Categories: Get All")
     .WithSummary("Recupera todas as categoria")
     .WithDescription("Recupera todas as categoria ")
     .WithOrder(5)
     .Produces<PagedResponse<List<Category>?>>();


    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetCategoriesQuery
        {
            UserId = "Matheuszin",
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
