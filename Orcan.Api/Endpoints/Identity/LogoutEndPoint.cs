using Microsoft.AspNetCore.Identity;
using Orcan.Api.Common.Api;
using Orcan.Api.Models;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using System.Security.Claims;

namespace Orcan.Api.Endpoints.Identity;

public class LogoutEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/logout", HandleAsync).RequireAuthorization();

    private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}
