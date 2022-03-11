using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetAsync();
        Task<UserModel> GetAsync(string id);
        Task<UserModel> CreateAsync(string userName, string firstName, string lastName, string password, string roleName);
        Task UpdateAsync(string id, string userName, string firstName, string lastName, string password, string roleName);
        Task DeleteAsync(string id);
    }
}
