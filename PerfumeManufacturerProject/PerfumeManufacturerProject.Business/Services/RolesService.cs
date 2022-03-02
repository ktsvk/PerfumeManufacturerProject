using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesService(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> GetAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleModel>>(roles);
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
        }

        public async Task DeleteAsync(string id) // if users with this role exists throw error
        {
            var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException(id);

            await _roleManager.DeleteAsync(role);
        }

        public Task AddPermissionAsync(string id, string name)
        {
            throw new System.NotImplementedException();
        }
        
        public Task DeletePermissionAsync(string id, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
