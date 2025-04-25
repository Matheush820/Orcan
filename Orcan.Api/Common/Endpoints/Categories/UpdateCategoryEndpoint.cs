using Orcan.Api.Common.Api;
using Orcan.Api.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Common.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
   => app.MapPut("/{id}", HandleAsync)
       .WithName("Categories: Update")
       .WithSummary("Atualiza uma categoria")
       .WithDescription("Categoria atualizada")
       .WithOrder(2)
       .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICategoryHandler handler,
        UpdateCategoryRequest request,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);

        return Results.BadRequest(result);
    }
}
