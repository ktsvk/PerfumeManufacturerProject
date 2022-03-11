using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminModel>> GetAsync();
        Task<AdminModel> GetAsync(string id);
        Task<AdminModel> CreateAsync(string userName, string firstName, string lastName, string password);
        Task UpdateAsync(string id, string userName, string firstName, string lastName, string password);
        Task DeleteAsync(string id);
    }
}
