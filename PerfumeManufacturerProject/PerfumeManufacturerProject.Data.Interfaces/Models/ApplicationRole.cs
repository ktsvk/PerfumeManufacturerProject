using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PerfumeManufacturerProject.Data.Interfaces.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<Permission> Permissions { get; set; }

        public ApplicationRole()
        {
            Permissions = new List<Permission>();
        }
    }
}
