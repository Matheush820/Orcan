﻿using Orçan.Api.Common.Api;
using Orçan.Api.Common.Endpoints.Categories;
using Orçan.Api.Common.Endpoints.Transactions;

namespace Orçan.Api.Common.Endpoints;

public static class EndPoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            //.RequireAuthorization()
            .MapEndPoint<CreateCategoryEndpoint>()
            .MapEndPoint<UpdateCategoryEndpoint>()
            .MapEndPoint<DeleteCategoryEndpoint>()
            .MapEndPoint<GetCategoryByIdEndpoint>()
            .MapEndPoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transaction")
           .WithTags("Categories")
           //.RequireAuthorization()
           .MapEndPoint<CreateTransactionEndpoint>()
           .MapEndPoint<UpdateTransactionEndpoint>()
           .MapEndPoint<DeleteTransactionEndpoint>()
           .MapEndPoint<GetTransactionByIdEndpoint>()
           .MapEndPoint<GetTransactionByPeriodEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndPoint<TEndoint>(this IEndpointRouteBuilder app)
        where TEndoint : IEndpoint
    {
        TEndoint.Map(app);
        return app;
    }
}
