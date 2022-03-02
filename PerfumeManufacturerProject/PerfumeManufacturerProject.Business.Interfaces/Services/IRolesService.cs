using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleModel>> GetAsync();
        Task<IEnumerable<PermissionModel>> GetPermissionsAsync();
        Task<RoleModel> CreateAsync(string name);
        Task UpdateAsync(string id, string name);
        Task DeleteAsync(string id);
        Task AddPermissionAsync(string roleId, string permissionId);
        Task DeletePermissionAsync(string roleId, string permissionId);
    }
}
