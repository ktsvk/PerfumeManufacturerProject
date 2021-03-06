using Microsoft.AspNetCore.Identity;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (result.Succeeded)
                {
                    return new LoginResult { Id = user.Id, UserName = user.UserName };
                }
            }
            throw new InvalidLoginException();
        }

        public async Task<LoginResult> RegisterAsync(string username, string password)
        {
            var user = new IdentityUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new LoginResult { Id = user.Id, UserName = user.UserName };
            }
            throw new ErrorDuringRegisterException(result.Errors);
        }

        public async Task LogoutAsync() =>
            await _signInManager.SignOutAsync();
    }
}
