using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Data.Interfaces.Models;

namespace PerfumeManufacturerProject.Data.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
