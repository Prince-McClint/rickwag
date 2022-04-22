using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WordJumble.Shared;

namespace WordJumble.Client.Services
{
    public interface IAccountsService
    {
        Task Signup(SignupRequest signupRequest);
        Task Login(LoginRequest loginRequest);
        Task Logout();
        Task<CurrentUser> GetCurrentUser();
        Task<bool> SavePlayerProfile(UserProfile profile);
        Task DeleteCurrentUserAccount();
    }
}
