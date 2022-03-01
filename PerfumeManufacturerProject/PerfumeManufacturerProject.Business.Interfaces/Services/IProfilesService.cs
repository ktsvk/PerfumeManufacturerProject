using PerfumeManufacturerProject.Business.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IProfilesService
    {
        Task<IEnumerable<ProfileModel>> GetAsync();
        Task<ProfileModel> CreateAsync(string name);
        Task UpdateAsync(string id, string name);
        Task DeleteAsync(string id);
        Task AddPermissionAsync(string id, string name);
        Task DeletePermissionAsync(string id, string name);
    }
}
