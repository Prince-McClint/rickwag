using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using WordJumble.Shared;
using WordJumble.Server.Models;

namespace WordJumble.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region fields
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;
        #endregion

        #region methods
        public AccountsController(UserManager<Player> _userManager, SignInManager<Player> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            //create new user
            var newUser = new Player()
            {
                UserName = signupRequest.Username
            };

            //add user to db with password
            var result = await userManager.CreateAsync(newUser, signupRequest.Password);
            if (!result.Succeeded) return BadRequest($"{result.Errors.FirstOrDefault()?.Description}");

            //automatic login after successfull signup
            return await Login(new LoginRequest()
            {
                Username = signupRequest.Username,
                Password = signupRequest.Password
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            //check if user by that username exists
            var user = await userManager.FindByNameAsync(loginRequest.Username);
            if (user == null) return BadRequest($"sorry, user with username {loginRequest.Username} doesn't exist");

            //check if password is correct
            var result = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if (!result.Succeeded) return BadRequest("sorry, the password entered is incorrect");

            //sign in
            await signInManager.SignInAsync(user, loginRequest.RememberMe);

            return Ok();
        }

        [HttpGet]
        public CurrentUser GetCurrentUser()
        {
            return new CurrentUser()
            {
                Username = User.Identity.Name,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SavePlayerProfile(UserProfile profile)
        {
            var beforeUsername = profile.PreviousUsername;
            var afterUsername = profile.Username;

            var player = await userManager.FindByNameAsync(beforeUsername);

            player.UserName = afterUsername;

            await userManager.UpdateAsync(player);

            var pass_result = await userManager.ChangePasswordAsync(player, profile.CurrentPassword, profile.Password);

            if (!pass_result.Succeeded)
            {
                Console.WriteLine("...............PASSWORD save Error!!!!!!!!!!!!!!!!!!!!!!");
                return BadRequest("sorry, current password is incorrect");
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCurrentUserAccount()
        {
            if (!User.Identity.IsAuthenticated) return Ok();

            var currentUser = GetCurrentUser();

            var currentPlayer = await userManager.FindByNameAsync(currentUser.Username);

            await userManager.DeleteAsync(currentPlayer);

            return Ok();
        }
        #endregion
    }
}
