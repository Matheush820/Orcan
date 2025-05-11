using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Transactions;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Transactions;

public class DeleteTransactionEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapDelete("/{id}", HandleAsync)
     .WithName("Transactions: Delete")
     .WithSummary("Deleta uma transação")
     .WithDescription("Transação Deletada")
     .WithOrder(3)
     .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
    {
        var request = new DeleteTransactionRequest
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
