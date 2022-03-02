using Microsoft.AspNetCore.Identity;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (await userManager.FindByEmailAsync("admin") == null)
            {
                var admin = new ApplicationUser { UserName = "admin" };
                var result = await userManager.CreateAsync(admin, "admin");
            }
        }
    }
}
