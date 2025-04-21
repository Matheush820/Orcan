using Azure;
using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using System.Security.Claims;

namespace Orçan.Api.Common.Endpoints.Categories;

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
