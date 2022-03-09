using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserModel> GetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);

            var role = await _roleManager.FindByNameAsync((await _userManager.GetRolesAsync(user)).FirstOrDefault());
            var userModel = _mapper.Map<UserModel>(user);
            userModel.Role = _mapper.Map<RoleModel>(role);
            return userModel;
        }

        public async Task<IEnumerable<UserModel>> GetAdminsAsync()
        {
            var userModels = new List<UserModel>();
            var users = await _userManager.GetUsersInRoleAsync("Admin");

            foreach (var user in users)
            {
                var role = await _roleManager.FindByNameAsync((await _userManager.GetRolesAsync(user)).FirstOrDefault());
                var roleModel = _mapper.Map<RoleModel>(role);
                var userModel = _mapper.Map<UserModel>(user);
                userModel.Role = roleModel;
                userModels.Add(userModel);
            }
            return userModels.OrderBy(x => x.LastName);
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var userModels = new List<UserModel>();

            var roles = _roleManager.Roles.Where(x => x.Name != "Admin");

            foreach (var role in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);

                foreach (var user in users)
                {
                    var userModel = _mapper.Map<UserModel>(user);
                    userModel.Role = _mapper.Map<RoleModel>(role);
                    userModels.Add(userModel);
                }
            }

            return userModels.OrderBy(x => x.LastName);
        }

        public async Task<UserModel> CreateAsync(string username, string password, string firstName, string lastName, string roleName)
        {
            if (await _userManager.FindByNameAsync(username) != null)
            {
                throw new UserAlreadyExistsException(username);
            }

            var role = await _roleManager.FindByNameAsync(roleName) ?? throw new RoleNotFoundException(roleName);

            var user = new ApplicationUser { UserName = username, FirstName = firstName, LastName = lastName };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
                await _userManager.UpdateAsync(user);

                var userModel = _mapper.Map<UserModel>(user);
                userModel.Role = _mapper.Map<RoleModel>(role);
                return userModel;
            }
            throw new ErrorDuringRegisterException(result.Errors);
        }

        public async Task UpdateAsync(string id, string firstName, string lastName, string userName, string password, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);

            if (!string.IsNullOrEmpty(password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName) ?? throw new RoleNotFoundException(roleName);
                var roles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, role.Name);
            }

            user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;
            user.LastName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;
            await _userManager.SetUserNameAsync(user, userName);
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateAdminAsync(string id, string firstName, string lastName, string userName, string password)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);

            if (!string.IsNullOrEmpty(password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
            }

            user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;
            user.LastName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;
            await _userManager.SetUserNameAsync(user, userName);
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);
            await _userManager.DeleteAsync(user);
        }
    }
}
