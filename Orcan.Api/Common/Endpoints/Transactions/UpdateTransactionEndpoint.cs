using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Transactions;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Common.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
  => app.MapPut("/{id}", HandleAsync)
      .WithName("Transactions: Update")
      .WithSummary("Atualiza uma transação")
      .WithDescription("Transação atualizada")
      .WithOrder(2)
      .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        UpdateTransactionRequest request, long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);

        return Results.BadRequest(result);
    }
}
