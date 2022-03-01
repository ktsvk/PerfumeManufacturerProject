using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Models;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.EF;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptModule = BCrypt.Net.BCrypt;

namespace PerfumeManufacturerProject.Business.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserModel> GetAsync(string id)
        {
            var user = await _context.Users
                .Include(x => x.Profile)
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new UserNotFoundException(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetAdminsAsync() =>
            await GetUsersAsync(true);

        public async Task<IEnumerable<UserModel>> GetUsersAsync() =>
            await GetUsersAsync(false);

        private async Task<IEnumerable<UserModel>> GetUsersAsync(bool isAdmin)
        {
            var users = _context.Users
                .Include(x => x.Profile)
                .Where(x => x.Profile.Name.Contains("Admin") == isAdmin)
                .OrderBy(x => x.LastName);

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> CreateAsync(string username, string password, string firstName, string lastName, string profileName)
        {
            if (await _context.Users.FirstOrDefaultAsync(x => x.UserName == username) != null)
            {
                throw new UserAlreadyExistsException(username);
            }

            var passwordHash = BCryptModule.HashPassword(password);

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Name == profileName) ?? throw new ProfileNotFoundException(profileName);

            var user = new User { UserName = username, FirstName = firstName, LastName = lastName, PasswordHash = passwordHash, Profile = profile };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userModel = _mapper.Map<UserModel>(user);
            return userModel;
        }

        public async Task UpdateAsync(string id, string firstName, string lastName, string profileName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new UserNotFoundException(id);

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Name == profileName) ?? throw new ProfileNotFoundException(profileName);

            user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;
            user.LastName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;
            user.Profile = profile;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new UserNotFoundException(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
