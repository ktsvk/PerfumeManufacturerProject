using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetAdminsAsync();
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetAsync(string id);
        Task<UserModel> CreateAsync(string username, string password, string firstName, string lastName, string roleName);
        Task UpdateAsync(string id, string firstName, string lastName, string roleName);
        Task UpdateAdminAsync(string id, string firstName, string lastName, string userName, string oldPassword, string newPassword);
        Task DeleteAsync(string id);
    }
}
