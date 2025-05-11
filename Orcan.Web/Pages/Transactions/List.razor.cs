using Microsoft.AspNetCore.Components;
using MudBlazor;
using Orcan.Core.Common.Extension;
using Orcan.Core.Handlers;
using Orcan.Core.Models;
using Orcan.Core.Requests.Transactions;

namespace Orcan.Web.Pages.Transactions;

public partial class ListTransactionsPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Transaction> Transactions { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    public int CurrentYear { get; set; } = DateTime.Now.Year;
    public int CurrentMonth { get; set; } = DateTime.Now.Month;

    public int[] Years { get; set; } =
    {
        DateTime.Now.Year,
        DateTime.Now.AddYears(-1).Year,
        DateTime.Now.AddYears(-2).Year,
        DateTime.Now.AddYears(-3).Year
    };
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ITransactionHandler Handler { get; set; } = null!;
    #endregion

    #region Override
    protected override async Task OnInitializedAsync()
        => await GetTransactions();
    #endregion

    #region Methods
    public Func<Transaction, bool> Filter => transaction =>
        string.IsNullOrWhiteSpace(SearchTerm) ||
        transaction.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);

    private async Task OnDeleteAsync(long id, string title)
    {
        IsBusy = true;

        try
        {
            var result = await Handler.DeleteAsync(new DeleteTransactionRequest
            {
                Id = id,
            });
            if (result.IsSuccess)
            {
                Snackbar.Add($"Lançamento {title} excluido com sucesso", Severity.Success);
                Transactions.RemoveAll(t => t.Id == id);
            }
            else
            {
                Snackbar.Add($"Ocorreu um erro ao excluir o lançamento {title}", Severity.Error);
            }
        }
        catch
        {
            Snackbar.Add($"Ocorreu um erro ao excluir o lançamento {title}", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Public Methods

    public async Task OnSearchAsync()
    {
        await GetTransactions();
        StateHasChanged();
    }

    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox("ATENÇÃO", $"Ao prosseguir o lançamento {title} será excluido. Esta ação é irreversivel", "EXCLUIR", "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();
    }
    private async Task GetTransactions()
    {
        IsBusy = true;

        try
        {
            var request = new GetTransactionsByPeriodRequest
            {
                StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                PageNumber = 1,
                PageSize = 1000,
            };

            var result = await Handler.GetByPeriod(request);
            Transactions = result.IsSuccess ? result.Data ?? [] : [];
        }
        catch
        {
            Snackbar.Add("Ocorreu um erro durante sua transação.", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
