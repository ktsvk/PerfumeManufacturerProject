using Microsoft.AspNetCore.Identity;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.Interfaces.Models.Auth;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username) ?? throw new InvalidLoginException();

            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                return new LoginResult { Id = user.Id, UserName = user.UserName };
            }

            throw new InvalidLoginException();
        }

        public async Task<LoginResult> RegisterAsync(string username, string password)
        {
            var user = new ApplicationUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new LoginResult { Id = user.Id, UserName = user.UserName };
            }

            throw new ErrorDuringRegisterException(result.Errors);
        }

        public async Task<LoginResult> GetMeAsync()
        {
            var userName = _signInManager.Context.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName) ?? throw new UserNotFoundException(userName);

            return new LoginResult { Id = user.Id, UserName = user.UserName };
        }

        public async Task LogoutAsync() =>
            await _signInManager.SignOutAsync();
    }
}
