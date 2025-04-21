using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orçan.Api.Data;
using Orçan.Api.Handlers;
using Orçan.Api.Models;
using Orçan.Core;
using Orçan.Core.Handlers;

namespace Orçan.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
       Configuration.ConnectionString = builder
            .Configuration.
            GetConnectionString("DefaultConnection");
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
        });
        builder.Services.AddEndpointsApiExplorer();
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        // Configuração do DbContext com o SQL Server
        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(Configuration.ConnectionString); // Agora UseSqlServer será reconhecido
        });

        builder.Services.AddIdentityCore<User>()
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
        builder.Services.AddCors(
            options => options.AddPolicy(ApiConfiguration.CorsPolicyName,
            policy =>
            {
                policy
                .WithOrigins([Configuration.BackendUrl, 
                    Configuration.FrontendUrl
                ])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }));
    }
}
