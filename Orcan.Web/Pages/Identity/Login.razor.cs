using Microsoft.AspNetCore.Components;
using MudBlazor;
using Orcan.Core.Handlers;
using Orcan.Core.Requests.Account;
using Orcan.Web.Security;

namespace Orcan.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Dependencies
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IACcountHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    #endregion

    #region Properties
    public bool IsBusy { get; set; } = false;
    public LoginRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;


        if (user.Identity is { IsAuthenticated: true})
        {

            if (NavigationManager.Uri.Contains("/comecar"))
            {
                return;
            }

            else
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
            }
        }
    }

    #endregion
    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess)
            {
                Console.WriteLine("SUCESSO AO SUBMETER FORMULARIO");
                NavigationManager.NavigateTo("/");

            }


            else
            {
                Console.WriteLine("ERRO AO SUBMETER FORMULARIO");
                Snackbar.Add(result.Message, Severity.Error);
            }
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
}
