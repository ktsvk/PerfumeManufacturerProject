using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Data.EF;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Data
{
    public static class DbInitializer
    {

        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (await userManager.FindByEmailAsync("admin") == null)
            {
                var admin = new ApplicationUser { UserName = "admin" };
                var result = await userManager.CreateAsync(admin, "admin");
            }
            if ((await context.Permissions.ToListAsync()).Count <= 0)
            {
                await context.Permissions.AddRangeAsync(
                    new Permission { Name = "Page1" },
                    new Permission { Name = "Page2" },
                    new Permission { Name = "Page3" },
                    new Permission { Name = "Page4" },
                    new Permission { Name = "Page5" },
                    new Permission { Name = "Page6" },
                    new Permission { Name = "Page7" },
                    new Permission { Name = "Page8" });
                await context.SaveChangesAsync();
            }
            if ((await context.Roles.ToListAsync()).Count <= 0)
            {
                var permissions = await context.Permissions.ToListAsync();
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin", Permissions = permissions.Where(x => x.Name == "Page1" || x.Name == "Page2").ToList() });
                await roleManager.CreateAsync(new ApplicationRole { Name = "Salesman", Permissions = permissions.Where(x => x.Name == "Page3" || x.Name == "Page5").ToList() });
                await roleManager.CreateAsync(new ApplicationRole { Name = "Manager", Permissions = permissions.Where(x => x.Name == "Page6" || x.Name == "Page8").ToList() });
                await roleManager.CreateAsync(new ApplicationRole { Name = "User", Permissions = permissions.Where(x => x.Name == "Page4" || x.Name == "Page7" || x.Name == "Page1").ToList() });
            }
        }
    }
}
