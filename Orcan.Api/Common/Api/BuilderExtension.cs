using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orcan.Api.Data;
using Orcan.Api.Handlers;
using Orcan.Api.Models;
using Orcan.Core;
using Orcan.Core.Handlers;

namespace Orcan.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;

    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(type => type.FullName);
        });
    }
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.LoginPath = "/v1/identity/login"; 
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax; 
                options.Cookie.SecurePolicy = CookieSecurePolicy.None; 
            });

        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(Configuration.ConnectionString);
        });

        builder.Services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
        builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
            options.AddPolicy(ApiConfiguration.CorsPolicyName, policy =>
                policy.WithOrigins(
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));  // Permite o envio de cookies
    }

}
