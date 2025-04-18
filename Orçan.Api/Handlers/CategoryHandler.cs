using Orçan.Api.Data;
using Orçan.Core.Models;
using Orçan.Core.Requests.Categories;
using Orçan.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
namespace Orçan.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<PagedResponse<List<Category?>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            // Logando o UserId para garantir que está correto
            Console.WriteLine($"Consultando categorias para o UserId: {request.UserId}");

            // A consulta principal
            var query = context
                .Categories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title);

            // Debug: Verificando se o filtro está correto
            var totalCount = await query.CountAsync(); // Total de categorias com o filtro UserId
            Console.WriteLine($"Total de categorias para o UserId {request.UserId}: {totalCount}");

            if (totalCount == 0)
            {
                Console.WriteLine("Nenhuma categoria encontrada.");
            }

            // Paginando os resultados
            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize) // Corrigindo a indexação de páginas
                .Take(request.PageSize) // Número de itens por página
                .ToListAsync();

            // Retornando a resposta paginada
            return new PagedResponse<List<Category?>>(
                categories,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }
        catch (Exception ex)
        {
            // Logando erro caso ocorra
            Console.WriteLine($"Erro: {ex.Message}");
            return new PagedResponse<List<Category?>>(
                null,
                500,
                "Não foi possível consultar as categorias"
            );
        }
    }




    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is not null
                ? new Response<Category?>(category)
                : new Response<Category?>(null, 404, "Categoria não encontrada");
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return new Response<Category?>(null, 500, "Erro ao buscar a categoria");
        }
    }

    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria criada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Category?>(null, 500, "Erro ao criar a categoria");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            // Remover o AsNoTracking para garantir que a entidade seja rastreada
            var category = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            // Atualizando as propriedades da categoria
            category.Title = request.Title;
            category.Description = request.Description;

            // Atualizar a categoria no contexto, garantindo que ela seja rastreada
            context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            // Salvar as alterações no banco de dados
            await context.SaveChangesAsync();

            return new Response<Category?>(category);
        }
        catch (Exception ex)
        {
            // Log de erro para diagnóstico
            Console.WriteLine($"Erro ao atualizar a categoria: {ex.Message}");
            return new Response<Category?>(null, 500, "Erro ao atualizar a categoria");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            // Remover o AsNoTracking para garantir que a entidade seja rastreada
            var category = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");

            // Remover a categoria do contexto
            context.Categories.Remove(category);

            // Salvar as alterações no banco de dados
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Categoria excluída com sucesso");
        }
        catch (Exception ex)
        {
            // Log de erro para diagnóstico
            Console.WriteLine($"Erro ao excluir a categoria: {ex.Message}");
            return new Response<Category?>(null, 500, "Erro ao excluir a categoria");
        }
    }


}
