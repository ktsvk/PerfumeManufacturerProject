using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PerfumeManufacturerProject.Data.Interfaces.Models
{
    public class ApplicationRole : IdentityRole
    {
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
