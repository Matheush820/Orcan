using Microsoft.AspNetCore.Identity;
using Orcan.Api.Common.Api;
using Orcan.Api.Models;
using Orcan.Core.Models.Account;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Identity;

public class GetRolesEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/roles", HandleAsync).RequireAuthorization();

    private static async Task<IResult> HandleAsync(ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => new RoleClaim
            {
               Issuer = c.Issuer,
               OriginalIssuer =  c.OriginalIssuer,
               Type =c.Type,
               Value = c.Value,
               ValueType =  c.ValueType
            });

        return await Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}
