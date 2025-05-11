using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Categories;

public class CreateCategoryEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
        .WithName("Categories: Create")
        .WithSummary("Cria uma nova cateogria")
        .WithDescription("Categoria criada")
        .WithOrder(1)
        .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, CreateCategoryRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty; ;
        var result = await handler.CreateAsync(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Created($"/v1/categories/{result.Data.Id}", result);

        return TypedResults.BadRequest(result);
    }
}
