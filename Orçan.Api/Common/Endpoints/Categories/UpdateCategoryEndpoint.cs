using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Responses;

namespace Orçan.Api.Common.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
   => app.MapPut("/{id}", HandleAsync)
       .WithName("Categories: Update")
       .WithSummary("Atualiza uma categoria")
       .WithDescription("Categoria atualizada")
       .WithOrder(2)
       .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.UserId = "Matheuszin";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);

        return Results.BadRequest(result);
    }
}
