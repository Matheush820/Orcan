using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Requests.Transactions;
using Orçan.Core.Responses;
using System.Security.Claims;

namespace Orçan.Api.Common.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
 => app.MapDelete("/{id}", HandleAsync)
     .WithName("Transaction: Delete")
     .WithSummary("Deleta uma transação")
     .WithDescription("Transação Deletada")
     .WithOrder(3)
     .Produces<Response<Transaction?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        long id)
    {
        var request = new DeleteTransactionRequest
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
