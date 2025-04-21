using Microsoft.AspNetCore.Identity;
using Orçan.Api.Common.Api;
using Orçan.Api.Models;
using System.Security.Claims;

namespace Orçan.Api.Common.Endpoints.Identity;

public class GetRolesEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/get", HandleAsync)
              .WithName("Identity: GetRoles")
              .RequireAuthorization();

    private static Task<IResult> HandleAsync(ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });

        return Task.FromResult(Results.Json(roles));
    }
}
