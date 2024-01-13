using Microsoft.AspNetCore.Components;
using Tangy_Client.Service.IService;
using Tangy_Models;

namespace Tangy_Client.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDTO SignUpRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;

            var result = await _authService.RegisterUser(SignUpRequest);

            if (result.IsRegisterationSuccessful)
            {
                //registration is successful
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //failure
                Errors = result.Errors;
                ShowRegistrationErrors = true;

            }
            IsProcessing = false;
        }
    }
}
