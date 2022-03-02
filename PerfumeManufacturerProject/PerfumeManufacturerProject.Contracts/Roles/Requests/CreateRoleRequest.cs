using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Roles.Requests
{
    public sealed class CreateRoleRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
