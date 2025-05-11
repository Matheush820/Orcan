using Microsoft.AspNetCore.Identity;
using Orcan.Api.Models;
using System.Security.Claims;

namespace Orcan.Api.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())  // Habilitar o Swagger somente em desenvolvimento
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();  // Exigir autenticação para ver a documentação Swagger
        }
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseCors(ApiConfiguration.CorsPolicyName);  // Certificar-se de que o CORS seja aplicado primeiro
        app.UseAuthentication();  // Configuração de autenticação
        app.UseAuthorization();   // Configuração de autorização
    }

}
