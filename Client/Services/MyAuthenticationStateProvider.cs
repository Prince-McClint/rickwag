using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public class MyAuthenticationStateProvider : AuthenticationStateProvider
    {
        #region fields
        private readonly IAccountsService accountsApi;
        #endregion

        #region methods
        public MyAuthenticationStateProvider(IAccountsService _accountsApi)
        {
            accountsApi = _accountsApi;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                var currentUser = await GetCurrentUser();
                if (currentUser.IsAuthenticated)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, currentUser.Username)
                    }.Concat(currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));

                    identity = new ClaimsIdentity(claims, "server authentication");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred {e.Message}");
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }


        public async Task<CurrentUser> GetCurrentUser()
        {
            return await accountsApi.GetCurrentUser();
        }

        public async Task Signup(SignupRequest signupRequest)
        {
            await accountsApi.Signup(signupRequest);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Login(LoginRequest loginRequest)
        {
            await accountsApi.Login(loginRequest);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await accountsApi.Logout();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        #endregion
    }
}
