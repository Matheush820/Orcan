﻿using Microsoft.AspNetCore.Mvc;
using Orcan.Api.Common.Api;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Responses;
using Orcan.Core;
using Orcan.Core.Requests.Transactions;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/", HandleAsync)
      .WithName("Transactions: Get All")
      .WithSummary("Recupera todas as transações")
      .WithDescription("Transações todas as categoriasa")
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

        var result = await handler.GetByPeriod(request);

        if (result.IsSuccess && result.Data is not null)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result);
    }
}
