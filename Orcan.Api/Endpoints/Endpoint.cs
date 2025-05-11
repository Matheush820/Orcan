using Orcan.Api.Common.Api;
using Orcan.Api.Endpoints.Categories;
using Orcan.Api.Endpoints.Identity;
using Orcan.Api.Endpoints.Transactions;
using Orcan.Api.Models;
using Orcan.Core.Requests.Categories;

namespace Orcan.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndPoint>()
            .MapEndpoint<UpdateCategoryEndPoint>()
            .MapEndpoint<DeleteCategoryEndPoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndPoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndPoint>()
            .MapEndpoint<DeleteTransactionEndPoint>()
            .MapEndpoint<GetTransactionByIdEndPoint>()
            .MapEndpoint<GetTransactionByPeriodEndPoint>();

        endpoints.MapGroup("v1/identity")
          .WithTags("Identity")
          .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
          .WithTags("Identity")
          .RequireAuthorization()
          .MapEndpoint<LogoutEndPoint>()
          .MapEndpoint<GetRolesEndPoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
