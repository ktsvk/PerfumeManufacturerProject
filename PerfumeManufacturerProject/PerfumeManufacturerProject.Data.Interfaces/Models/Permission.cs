using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeManufacturerProject.Data.Interfaces.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationRole> Roles { get; set; }

        public Permission()
        {
            Roles = new List<ApplicationRole>();
        }
    }
}
