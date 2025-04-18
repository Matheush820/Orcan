using Orçan.Api.Data;
using Microsoft.EntityFrameworkCore;
using Orçan.Api.Handlers;
using Orçan.Api.Common.Endpoints;
using Orçan.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Orçan.Api.Models;
using System.Security.Claims; // Adicionando a diretiva necessária

var builder = WebApplication.CreateBuilder(args);

// Obter a string de conexão do arquivo appsettings.json
var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();

// Configuração do DbContext com o SQL Server
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnStr); // Agora UseSqlServer será reconhecido
});

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

// Adicionando os serviços ao cont0ainer de injeção de dependências
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Adicionando o Handler à injeção de dependência
builder.Services.AddScoped<ICategoryHandler, CategoryHandler>(); // Usando AddScoped para garantir o ciclo correto
builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();          // Para usar OpenAPI (caso necessário)
    app.UseSwagger();          // Habilita o Swagger
    app.UseSwaggerUI();        // Habilita a interface SwaggerUI
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => new {message = "OK"});
app.MapEndpoints();
app.MapGroup("v1/identity").WithTags("Identity").MapIdentityApi<User>();

app.MapGroup("v1/identity").WithTags("Identity").MapPost("/logout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();



app.MapGroup("v1/identity").WithTags("Identity").MapGet("/roles", (ClaimsPrincipal user) =>
{
    if (user.Identity is null || !user.Identity.IsAuthenticated)
        return Results.Unauthorized();

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

    return TypedResults.Json(roles);

}).RequireAuthorization();


app.Run();
