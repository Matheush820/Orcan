using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Responses;

namespace Orçan.Api.Common.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
  => app.MapDelete("/{id}", HandleAsync)
      .WithName("Categories: Delete")
      .WithSummary("Deleta uma categoria")
      .WithDescription("Categoria Deletada")
      .WithOrder(3)
      .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = "Matheuszin",
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
