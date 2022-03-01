using PerfumeManufacturerProject.Business.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetAdminsAsync();
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetAsync(string id);
        Task<UserModel> CreateAsync(string username, string password, string firstName, string lastName, string profileName);
        Task UpdateAsync(string id, string firstName, string lastName, string profileName);
        Task DeleteAsync(string id);
    }
}
