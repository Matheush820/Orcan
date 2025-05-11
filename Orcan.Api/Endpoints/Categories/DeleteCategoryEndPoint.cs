using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Categories;

public class DeleteCategoryEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
       => app.MapDelete("/{id}", HandleAsync)
       .WithName("Categories: Delete")
       .WithSummary("Deleta uma cateogria")
       .WithDescription("Categoria Deletada")
       .WithOrder(3)
       .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result);
    }
}