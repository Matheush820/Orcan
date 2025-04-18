using Orçan.Api.Common.Api;
using Orçan.Api.Handlers;
using Orçan.Core.Handlers;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Requests.Transactions;
using Orçan.Core.Responses;

namespace Orçan.Api.Common.Endpoints.Transactions;

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
        ITransactionHandler handler,
        CreateTransactionRequest request)
    {
        request.UserId = "Matheuszin";
        var result = await handler.CreateAsync(request);
        if (result.IsSuccess)
            return Results.Created($"/{result.Data?.Id}", result);

        return Results.BadRequest(result);
    }
}
