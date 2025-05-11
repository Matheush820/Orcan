using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Transactions;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Transactions;

public class GetTransactionByIdEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/{id}", HandleAsync)
      .WithName("Transactions: Get By Id")
      .WithSummary("Recupera uma transação pelo Id")
      .WithDescription("Transação Recuperada")
      .WithOrder(4)
      .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdRequest(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result);
    }
}
