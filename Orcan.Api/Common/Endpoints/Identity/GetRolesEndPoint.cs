﻿using Orcan.Api.Common.Api;
using Orcan.Core.Models.Account;
using System.Security.Claims;

namespace Orcan.Api.Common.Endpoints.Identity;

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
            .Select(c => new RoleClaim
            {
                Issuer = c.Issuer,
                OriginalIssuer= c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            });

        return Task.FromResult(Results.Json(roles));
    }
}
