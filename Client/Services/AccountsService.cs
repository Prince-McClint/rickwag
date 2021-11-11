using System.Net.Http.Json;

using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    
    public class AccountsService : IAccountsService
    {
        #region fields
        private readonly HttpClient httpClient;
        #endregion

        #region methods
        public AccountsService(HttpClient _httpclient)
        {
            httpClient = _httpclient;
        }

        public async Task<CurrentUser> GetCurrentUser()
        {
            var requestUri = "api/accounts/getcurrentuser";

            return await httpClient.GetFromJsonAsync<CurrentUser>(requestUri);
        }

        public async Task Login(LoginRequest loginRequest)
        {
            var requestUri = "api/accounts/login";

            var result = await httpClient.PostAsJsonAsync(requestUri, loginRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Signup(SignupRequest signupRequest)
        {
            var requestUri = "api/accounts/signup";

            var result = await httpClient.PostAsJsonAsync(requestUri, signupRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var requestUri = "api/accounts/logout";

            var result = await httpClient.PostAsync(requestUri, null);
            result.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
