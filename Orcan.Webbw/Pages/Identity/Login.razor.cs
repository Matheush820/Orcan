using Microsoft.AspNetCore.Components;
using MudBlazor;
using Orcan.Core.Handlers;
using Orcan.Core.Requests.Account;
using Orcan.Webbw.Security;

namespace Orcan.Webbw.Pages.Identity
{
    public partial class Login : ComponentBase
    {
        #region Injects
        [Inject]
        public ISnackbar Snackbar { get; set; } = null;
        [Inject]
        public IAccountHandler Handler { get; set; } = null;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null;
        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null;
        #endregion

        #region Properties
        public bool IsBusy { get; set; }
        public LoginRequest InputModel { get; set; }
        #endregion

        #region Overrides
        protected override void OnInitialized()
        {
            InputModel = new LoginRequest(); // Inicializando o modelo para o formulário
        }

        protected override async Task OnInitializedAsync()
        {
            // Inicializa o modelo aqui, antes da renderização
            InputModel = new LoginRequest();

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
                NavigationManager.NavigateTo("/"); // Se já estiver autenticado, redireciona para a página inicial
        }

        #endregion

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.LoginAsync(InputModel);

                if (result.IsSuccess)
                {
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                    NavigationManager.NavigateTo("/inicial"); // Após login bem-sucedido, redireciona para a página inicial
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error); // Corrigido para Severity.Error, pois é um erro
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
