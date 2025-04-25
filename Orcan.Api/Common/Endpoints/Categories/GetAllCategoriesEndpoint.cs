using Microsoft.AspNetCore.Mvc;
using Orcan.Api.Common.Api;
using Orcan.Api.Handlers;
using Orcan.Core;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories.Herança;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Common.Endpoints.Categories;

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
        ClaimsPrincipal user,
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetCategoriesQuery
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await handler.GetAllAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
