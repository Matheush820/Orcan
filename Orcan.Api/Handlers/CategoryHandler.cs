using Microsoft.EntityFrameworkCore;
using Orcan.Api.Data;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;
using System.Diagnostics.Contracts;

namespace Orcan.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };

            await context.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria atualizada");

        }
        catch
        {
            return new Response<Category?>(null, 505, "Não foi possivel criar uma categoria");

        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada para deletar");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(null, 200, "Categoria deletada");
        }
        catch
        {
            return new Response<Category?>(null, 404, "Não foi possivel excluir essa categoria");
        }
    }

    public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context
               .Categories.AsNoTracking()
               .Where(x => x.UserId == request.UserId)
               .OrderBy(x => x.Title);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>>(
                categories,
                count,
                request.PageNumber,
                request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Category>>(new List<Category>(), 500, "Não foi possivel listar todas as categorias");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            if (request == null || request.Id <= 0)
                return new Response<Category?>(null, 400, "Requisição inválida");

            var category = await context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return category is null
                ? new Response<Category?>(null, 404, "Categoria não encontrada")
                : new Response<Category?>(category, 200, "Categoria encontrada");
        }
        catch (Exception ex)
        {
            return new Response<Category?>(null, 500, $"Erro ao recuperar categoria: {ex.Message}");
        }
    }


    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada para atualização");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(null, 200, "Categoria atualizada");
        }
        catch
        {
            return new Response<Category?>(null, 404, "Categoria Não encontrada");

        }

    }
}
