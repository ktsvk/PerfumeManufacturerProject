using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.Interfaces.Models.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AdminModel> GetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);

            return _mapper.Map<AdminModel>(user);
        }

        public async Task<IEnumerable<AdminModel>> GetAsync()
        {
            var users = await _userManager.Users.OrderBy(x => x.UserName).ToListAsync();

            return _mapper.Map<IEnumerable<AdminModel>>(users);
        }

        public async Task<AdminModel> CreateAsync(string userName, string firstName, string lastName, string password)
        {
            if (await _userManager.FindByNameAsync(userName) != null)
            {
                throw new UserAlreadyExistsException(userName);
            }

            var user = new ApplicationUser { UserName = userName, FirstName = firstName, LastName = lastName };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return _mapper.Map<AdminModel>(user);
            }
            throw new ErrorDuringRegisterException(result.Errors);
        }

        public async Task UpdateAsync(string id, string userName, string firstName, string lastName, string password)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);

            if (!string.IsNullOrEmpty(password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
            }

            user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;
            user.LastName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;

            if (!string.IsNullOrEmpty(userName))
            {
                var result = await _userManager.SetUserNameAsync(user, userName);

                if (!result.Succeeded)
                {
                    throw new ErrorDuringRegisterException(result.Errors);
                }
            }

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException(id);

            await _userManager.DeleteAsync(user);
        }
    }
}
