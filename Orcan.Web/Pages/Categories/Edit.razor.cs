using Microsoft.AspNetCore.Components;
using MudBlazor;
using Orcan.Core.Handlers;
using Orcan.Core.Requests.Categories;
using System.Security.Cryptography.X509Certificates;

namespace Orcan.Web.Pages.Categories;

public partial class EditCategoryPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public UpdateCategoryRequest InputModel { get; set; } = new();
    #endregion

    #region Parameters
    [Parameter]
    public string Id { get; set; } = string.Empty;

    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        GetCategoryByIdRequest? request = null;
        try
        {
            request = new GetCategoryByIdRequest
            {
                Id = long.Parse(Id)
            };
        }
        catch
        {
            Snackbar.Add("Id inválido", Severity.Error);
        }

        // aqui você precisa checar e retornar, senão o código abaixo quebra
        if (request is null)
            return;

        IsBusy = true;
        try
        {
            var response = await Handler.GetByIdAsync(request);
            if (response.IsSuccess && response.Data is not null)
            {
                InputModel = new UpdateCategoryRequest
                {
                    Id = response.Data.Id,
                    Title = response.Data.Title,
                    Description = response.Data.Description
                };
            }
        }
        catch
        {
            Snackbar.Add("Erro ao buscar categoria", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }

        #endregion

    }
        #region
        public async Task OnValidSubmitAsync()
            {
            IsBusy = true;

        try
        {
            var result = await Handler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add("Categoria atualizada com sucesso", Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
        }
        catch
        {
            Snackbar.Add("Erro ao atualizar categoria", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
        #endregion
}
