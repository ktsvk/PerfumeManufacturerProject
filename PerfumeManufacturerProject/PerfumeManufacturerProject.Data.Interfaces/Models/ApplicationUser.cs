using Microsoft.AspNetCore.Identity;

namespace PerfumeManufacturerProject.Data.Interfaces.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
