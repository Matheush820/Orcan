using Orcan.Api.Common.Api;
using Orcan.Api.Common.Endpoints.Categories;
using Orcan.Api.Common.Endpoints.Identity;
using Orcan.Api.Common.Endpoints.Transactions;
using Orcan.Api.Models;

namespace Orcan.Api.Common.Endpoints;

public static class EndPoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health check").MapGet("/", () => new { message = "OK" });
        ;
        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndPoint<CreateCategoryEndpoint>()
            .MapEndPoint<UpdateCategoryEndpoint>()
            .MapEndPoint<DeleteCategoryEndpoint>()
            .MapEndPoint<GetCategoryByIdEndpoint>()
            .MapEndPoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transaction")
           .WithTags("Categories")
           .RequireAuthorization()
           .MapEndPoint<CreateTransactionEndpoint>()
           .MapEndPoint<UpdateTransactionEndpoint>()
           .MapEndPoint<DeleteTransactionEndpoint>()
           .MapEndPoint<GetTransactionByIdEndpoint>()
           .MapEndPoint<GetTransactionByPeriodEndpoint>();

            endpoints.MapGroup("v1/identity")
           .WithTags("Identity")
           .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
       .WithTags("Identity")
       .MapEndPoint<LogoutEndPoint>()
       .MapEndPoint<GetRolesEndPoint>();
    }

    private static IEndpointRouteBuilder MapEndPoint<TEndoint>(this IEndpointRouteBuilder app)
        where TEndoint : IEndpoint
    {
        TEndoint.Map(app);
        return app;
    }
}
