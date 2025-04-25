using Microsoft.AspNetCore.Identity;
using Orcan.Api.Common.Api;
using Orcan.Api.Models;

namespace Orcan.Api.Common.Endpoints.Identity;

public class LogoutEndPoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
     => app.MapPost("/logout", HandleAsync)
         .WithName("Identity: Create").RequireAuthorization();

    private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }

}
