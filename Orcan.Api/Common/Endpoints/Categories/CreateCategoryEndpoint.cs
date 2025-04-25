using Orcan.Api.Common.Api;
using Orcan.Api.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Common.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
        .WithName("Categories: Create")
        .WithSummary("Cria uma nova categoria")
        .WithDescription("Categoria criada")
        .WithOrder(1)
        .Produces<Response<Category?>>();
        


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        CreateCategoryRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty; ;
        var result = await handler.CreateAsync(request);
        if(result.IsSuccess)
            return Results.Created($"/{result.Data?.Id}", result);

        return Results.BadRequest(result);
    }
}
