using Microsoft.AspNetCore.Mvc;
using Orcan.Api.Common.Api;
using Orcan.Core;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Categories;

public class GetAllCategoriesEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/", HandleAsync)
      .WithName("Categories: Get All")
      .WithSummary("Recupera todas as categorias")
      .WithDescription("Categoria todas as categoriasa")
      .WithOrder(5)
      .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result);
    }
}
