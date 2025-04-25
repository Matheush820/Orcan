namespace Orcan.Api.Common.Api;

public static class AppEntension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecutiry(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

    }
}
