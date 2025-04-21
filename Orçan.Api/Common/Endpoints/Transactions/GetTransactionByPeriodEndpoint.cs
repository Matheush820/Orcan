using Microsoft.AspNetCore.Mvc;
using Orçan.Api.Common.Api;
using Orçan.Core.Models;
using Orçan.Core.Responses;
using Orçan.Core;
using Orçan.Core.Handlers;
using Orçan.Core.Requests.Transactions;
using System.Security.Claims;

namespace Orçan.Api.Common.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
=> app.MapGet("/", HandleAsync)
   .WithName("Transactions: Get All")
   .WithSummary("Recupera todas as transações")
   .WithDescription("Recupera todas as transações ")
   .WithOrder(5)
   .Produces<PagedResponse<List<Transaction>?>>();


    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITransactionHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionsByPeriodRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };
        var result = await handler.GetTransactionsByPeriodAsync(request);
        if (result.IsSuccess)
            return Results.Ok(result);
        return Results.BadRequest(result);
    }
}
