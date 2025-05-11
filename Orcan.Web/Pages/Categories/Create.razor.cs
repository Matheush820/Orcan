using Microsoft.AspNetCore.Components;
using MudBlazor;
using Orcan.Core.Handlers;
using Orcan.Core.Requests.Categories;
using Orcan.Web.Handlers;

namespace Orcan.Web.Pages.Categories;

public partial class CreateCategoryPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CreateCategoryRequest InputModel { get; set; } = new();
    public ISnackbar Snackbar { get; set; }
    #endregion

    #region Services
    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if(result.IsSuccess)
                NavigationManager.NavigateTo("/categorias");

            else Snackbar.Add(result.Message, Severity.Error);
        }
        catch(Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
