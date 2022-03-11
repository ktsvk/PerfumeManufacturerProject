using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Data.Interfaces.Models;

namespace PerfumeManufacturerProject.Data.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
