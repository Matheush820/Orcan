using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Responses;
using System.Security.Claims;

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


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
