using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleModel>> GetAsync();
        Task<RoleModel> CreateAsync(string name);
        Task UpdateAsync(string id, string name);
        Task DeleteAsync(string id);
        Task AddPermissionAsync(string id, string name);
        Task DeletePermissionAsync(string id, string name);
    }
}
