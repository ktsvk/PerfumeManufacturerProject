using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Roles.Requests
{
    public sealed class UpdateRoleRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
