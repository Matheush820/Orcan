using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Requests.Transactions;
using Orcan.Core.Responses;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapPost("/", HandleAsync)
      .WithName("Transactions: Create")
      .WithSummary("Cria uma nova transação")
      .WithDescription("Transação criada")
      .WithOrder(1)
      .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        CreateTransactionRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Created($"/v1/categories/{result.Data.Id}", result);

        return TypedResults.BadRequest(result);
    }
}
