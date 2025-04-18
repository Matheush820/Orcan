using Azure;
using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;

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
        ICategoryHandler handler,
        CreateCategoryRequest request)
    {
        request.UserId = "Matheuszin";
        var result = await handler.CreateAsync(request);
        if(result.IsSuccess)
            return Results.Created($"/{result.Data?.Id}", result);

        return Results.BadRequest(result);
    }
}
