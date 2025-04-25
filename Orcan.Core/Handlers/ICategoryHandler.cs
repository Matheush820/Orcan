using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Responses;

namespace Orcan.Api.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);

}
