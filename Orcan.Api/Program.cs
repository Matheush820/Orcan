using Orcan.Api;
using Orcan.Api.Common.Api;
using Orcan.Api.Common.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

// Adicionando os serviços ao cont0ainer de injeção de dependências
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();          // Para usar OpenAPI (caso necessário)
    app.ConfigureDevEnvironment();
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseHttpsRedirection();
app.UseSecutiry();
app.MapControllers();

app.MapEndpoints();

app.Run();
