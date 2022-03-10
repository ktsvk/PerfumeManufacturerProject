using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.EF;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _usersManager;
        private readonly IMapper _mapper;

        public RolesService(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, IMapper mapper, UserManager<ApplicationUser> usersManager)
        {
            _context = context;
            _roleManager = roleManager;
            _mapper = mapper;
            _usersManager = usersManager;
        }

        public async Task<IEnumerable<RoleModel>> GetAsync()
        {
            var roles = await _roleManager.Roles.Include(x => x.Permissions).ToListAsync();
            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task<IEnumerable<PermissionModel>> GetPermissionsAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            return _mapper.Map<IEnumerable<PermissionModel>>(permissions);
        }

        public async Task<RoleModel> CreateAsync(string name)
        {
            if (await _roleManager.RoleExistsAsync(name))
                throw new RoleAlreadyExistsException(name);

            var role = new ApplicationRole { Name = name };
            await _roleManager.CreateAsync(role);

            return _mapper.Map<RoleModel>(role);
        }

        public async Task UpdateAsync(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException(id);

            await _roleManager.SetRoleNameAsync(role, name);
            await _roleManager.UpdateAsync(role);
        }

        public async Task DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException(id);

            var usersInRole = await _usersManager.GetUsersInRoleAsync(role.Name);

            if (usersInRole.Count() > 0)
                throw new CannotDeleteRoleException(role.Name);

            await _roleManager.DeleteAsync(role);
        }

        public async Task AddPermissionAsync(string roleId, string permissionId)
        {
            var role = await _roleManager.Roles.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == roleId) ?? throw new RoleNotFoundException(roleId);
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id.ToString() == permissionId) ?? throw new PermissionNotFoundException(permissionId);

            role.Permissions.Add(permission);
            await _roleManager.UpdateAsync(role);
        }
        
        public async Task DeletePermissionAsync(string roleId, string permissionId)
        {
            var role = await _roleManager.Roles.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == roleId) ?? throw new RoleNotFoundException(roleId);
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id.ToString() == permissionId) ?? throw new PermissionNotFoundException(permissionId);

            role.Permissions.Remove(permission);
            await _roleManager.UpdateAsync(role);
        }
    }
}
