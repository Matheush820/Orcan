using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Requests.Transactions;
using Orçan.Core.Responses;

namespace Orçan.Api.Common.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
 => app.MapGet("/{id}", HandleAsync)
     .WithName("Transaction: Get By Id")
     .WithSummary("Recupera uma transação")
     .WithDescription("Recupera uma transação ")
     .WithOrder(4)
     .Produces<Response<Transaction?>>();


    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest
        {
            UserId = "Matheuszin",
            Id = id
        };
        var result = await handler.GetByIdAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
