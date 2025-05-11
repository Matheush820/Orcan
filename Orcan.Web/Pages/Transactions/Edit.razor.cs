﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Categories;
using Orcan.Core.Requests.Transactions;

namespace Orcan.Web.Pages.Transactions;

public class EditTransactionPage : ComponentBase
{
    #region Properties
    [Parameter]
    public string Id { get; set; } = string.Empty;
    public bool IsBusy { get; set; } = false;
    public UpdateTransactionRequest InputModel { get; set; } = new();
    public List<Category> Categories { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public ITransactionHandler TransactionHandler { get; set; } = null!;
    [Inject]
    public ICategoryHandler CategoryHandler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
       await GetTransactionByIdAsync();
        await GetCategoriesAsync();

        IsBusy = false;
        
    }

    #endregion
    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await TransactionHandler.UpdateAsync(InputModel);
            if (result.IsSuccess)
            { 
            Snackbar.Add("Lançamento atualizado com sucesso", Severity.Success);
                NavigationManager.NavigateTo("/lancamentos/historico");
            }

            else Snackbar.Add(result.Message, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region PrivateMethods

    private async Task GetTransactionByIdAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetTransactionByIdRequest { Id = long.Parse(Id)};
            var result = await TransactionHandler.GetByIdRequest(request);

            if (result.IsSuccess && result.Data is not null)
            {
                InputModel = new UpdateTransactionRequest
                {
                    CategoryId = result.Data.CategoryId,
                    PaiOrReceivedAt = result.Data.PaidOrReceivedAt,
                    Title = result.Data.Title,
                    Type = result.Data.Type,
                    Amount = result.Data.Amount,
                    Id = result.Data.Id

                };
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch
        {
            Snackbar.Add("Erro ao carregar categorias", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task GetCategoriesAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await CategoryHandler.GetAllAsync(request);

            if (result.IsSuccess)
            {
                Categories = result.Data ?? [];
                InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch
        {
            Snackbar.Add("Erro ao carregar categorias", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
