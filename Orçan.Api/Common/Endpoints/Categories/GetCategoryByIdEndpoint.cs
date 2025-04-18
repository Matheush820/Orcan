using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Responses;

namespace Orçan.Api.Common.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/{id}", HandleAsync)
      .WithName("Categories: Get By Id")
      .WithSummary("Recupera uma categoria")
      .WithDescription("Recupera uma categoria ")
      .WithOrder(4)
      .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId = "Matheuszin",
            Id = id
        };
        var result = await handler.GetByIdAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
