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
        private readonly IMapper _mapper;

        public RolesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> GetAsync()
        {
            var roles = await _context.Roles
                .Include(x => x.Permissions)
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task<IEnumerable<PermissionModel>> GetPermissionsAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();

            return _mapper.Map<IEnumerable<PermissionModel>>(permissions);
        }

        public async Task<RoleModel> CreateAsync(string name)
        {
            if (await _context.Roles.FirstOrDefaultAsync(x => x.Name == name) != null)
                throw new RoleAlreadyExistsException(name);

            var role = new Role { Name = name };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleModel>(role);
        }

        public async Task UpdateAsync(string id, string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new RoleNotFoundException(id);

            if (!string.IsNullOrEmpty(name))
            {
                role.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string id)
        {
            var role = await _context.Roles
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new RoleNotFoundException(id);

            if (role.Users.Count() > 0)
                throw new CannotDeleteRoleException(role.Name);

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task AddPermissionAsync(string roleId, string permissionId)
        {
            var role = await _context.Roles
                .Include(x => x.Users)
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id.ToString() == roleId) ?? throw new RoleNotFoundException(roleId);

            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id.ToString() == permissionId) ?? throw new PermissionNotFoundException(permissionId);

            role.Permissions.Add(permission);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeletePermissionAsync(string roleId, string permissionId)
        {
            var role = await _context.Roles
                .Include(x => x.Users)
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id.ToString() == roleId) ?? throw new RoleNotFoundException(roleId);

            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id.ToString() == permissionId) ?? throw new PermissionNotFoundException(permissionId);

            role.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }
}
