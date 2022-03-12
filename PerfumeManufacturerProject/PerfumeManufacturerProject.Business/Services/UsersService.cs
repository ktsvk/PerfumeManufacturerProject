using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
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
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new UserNotFoundException(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetAsync()
        {
            var users = await _context.Users
                .Include(x => x.Role)
                .OrderBy(x => x.UserName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> CreateAsync(string userName, string firstName, string lastName, string password, string roleName)
        {
            if (await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName) != null)
            {
                throw new UserAlreadyExistsException(userName);
            }

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName) ?? throw new RoleNotFoundException(roleName);

            var passwordHash = BCryptModule.HashPassword(password);

            var user = new User { UserName = userName, FirstName = firstName, LastName = lastName, PasswordHash = passwordHash, Role = role };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateAsync(string id, string userName, string firstName, string lastName, string password, string roleName)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new UserNotFoundException(id);

            if (!string.IsNullOrEmpty(password))
            {
                var passwordHash = BCryptModule.HashPassword(password);

                user.PasswordHash = passwordHash;
                await _context.SaveChangesAsync();
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName) ?? throw new RoleNotFoundException(roleName);
                user.Role = role;
                await _context.SaveChangesAsync();
            }

            user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;
            user.LastName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(userName))
            {
                if (await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName) != null)
                {
                    throw new System.Exception("this userName already exists"); // add new exception userName already exists
                }
                else
                {
                    user.UserName = userName;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new UserNotFoundException(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
