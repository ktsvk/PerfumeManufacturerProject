using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Data.Interfaces.Models.Auth;

namespace PerfumeManufacturerProject.Data.EF
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {

        }
    }
}
