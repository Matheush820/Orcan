using Orcan.Api.Common.Api;
using Orcan.Api.Endpoints;
using Orcan.Api;

var builder = WebApplication.CreateBuilder(args);

// Configurações e serviços
builder.AddConfiguration(); // Lembre-se de corrigir o nome para AddConfiguration
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

builder.Services.AddControllers();
builder.Services.AddOpenApi();  // Configure OpenAPI para documentação

var app = builder.Build();

// Ambiente de desenvolvimento (Swagger)
if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

// Pipeline de Middlewares
app.UseCors(ApiConfiguration.CorsPolicyName);  // Aplique CORS antes da autenticação
app.UseHttpsRedirection();  // Redirecionamento HTTPS
app.UseSecurity();  // Configura autenticação e autorização
app.MapControllers();  // Mapeamento dos controladores
app.MapEndpoints();  // Mapeamento dos endpoints

app.Run();  // Inicia o servidor
