using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Requests.Transactions;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Transactions;

public class UpdateTransactionEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
        .WithName("Transactions: Update")
        .WithSummary("Atualiza uma transação")
        .WithDescription("Transação Atualizada")
        .WithOrder(2)
        .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;

        var result = await handler.UpdateAsync(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result);
    }
}
