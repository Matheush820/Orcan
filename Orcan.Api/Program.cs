using Orcan.Api.Common.Api;
using Orcan.Api.Endpoints;
using Orcan.Api;

var builder = WebApplication.CreateBuilder(args);

// Configura��es e servi�os
builder.AddConfiguration(); // Lembre-se de corrigir o nome para AddConfiguration
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

builder.Services.AddControllers();
builder.Services.AddOpenApi();  // Configure OpenAPI para documenta��o

var app = builder.Build();

// Ambiente de desenvolvimento (Swagger)
if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

// Pipeline de Middlewares
app.UseCors(ApiConfiguration.CorsPolicyName);  // Aplique CORS antes da autentica��o
app.UseHttpsRedirection();  // Redirecionamento HTTPS
app.UseSecurity();  // Configura autentica��o e autoriza��o
app.MapControllers();  // Mapeamento dos controladores
app.MapEndpoints();  // Mapeamento dos endpoints

app.Run();  // Inicia o servidor
