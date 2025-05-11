using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Categories;

public class UpdateCategoryEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
        .WithName("Categories: Update")
        .WithSummary("Atualiza uma cateogria")
        .WithDescription("Categoria Atualizada")
        .WithOrder(2)
        .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;

        var result = await handler.UpdateAsync(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result);
    }
}
