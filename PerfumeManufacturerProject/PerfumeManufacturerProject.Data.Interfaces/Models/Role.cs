using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeManufacturerProject.Data.Interfaces.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Permission> Permissions { get; set; }

        public Role()
        {
            Permissions = new List<Permission>();
        }
    }
}
