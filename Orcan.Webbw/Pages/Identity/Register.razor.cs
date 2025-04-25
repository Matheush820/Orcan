using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Orcan.Core.Handlers;
using Orcan.Core.Requests.Account;

namespace Orcan.Webbw.Pages.Identity;

public partial class Register : ComponentBase
{
    #region
   [Inject] public ISnackbar Snackbar { get; set; }= null;
    [Inject] public IAccountHandler Handler { get; set; } = null;
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null;

    #endregion


    #region Properties
    public bool IsBusy { get; set; } = false;
    public RegisterRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        var authState  = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if(user.Identity is {IsAuthenticated: true})
            NavigationManager.NavigateTo("/login");
    }
    #endregion


    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.RegisterAsync(InputModel);

            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
            NavigationManager.NavigateTo("/login");
            }

            else
                Snackbar.Add(result.Message, Severity.Error);

        }catch(Exception e)
        {
            Snackbar.Add(e.Message, Severity.Success);

        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
