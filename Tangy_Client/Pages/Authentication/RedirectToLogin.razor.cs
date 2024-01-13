using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Tangy_Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [CascadingParameter]
        public Task<AuthenticationState> _authState { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        bool notAuthorized { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await _authState;

            //ถ้ายังไม่เข้าระบบ
            if (authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                //จดจำ Url ปัจจุบันไว้
                var returnUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    //ถ้าไม่มีพาทปัจจุบัน ให้ไปหน้า login
                    _navigationManager.NavigateTo("login");
                }
                else
                {
                    //ถ้ามีพาทปัจจุบัน ให้ไปหน้า login พร้อมกับส่งพารามิเตอร์ returnUrl ไปให้ด้วย
                    _navigationManager.NavigateTo($"login?returnUrl={returnUrl}");
                }
            }
            else
            {
                notAuthorized = true;
            }

        }
    }
}