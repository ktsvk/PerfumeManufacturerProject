using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(string username, string password);
        Task<LoginResult> RegisterAsync(string username, string password);
        Task<LoginResult> GetMeAsync();
        Task LogoutAsync();
    }
}
