using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync((await _userManager.GetRolesAsync(user)).FirstOrDefault());
                    var roleModel = _mapper.Map<RoleModel>(role);

                    return new LoginResult { Id = user.Id, UserName = user.UserName, Role = roleModel };
                }
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

            var role = await _roleManager.FindByNameAsync((await _userManager.GetRolesAsync(user)).FirstOrDefault());
            var roleModel = _mapper.Map<RoleModel>(role);

            return new LoginResult { Id = user.Id, UserName = user.UserName, Role = roleModel };
        }

        public async Task LogoutAsync() =>
            await _signInManager.SignOutAsync();
    }
}
