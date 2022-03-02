using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Roles.Requests
{
    public sealed class AddPermissionRequest
    {
        [Required]
        public string RoleId { get; set; }
        [Required]
        public string PermissionId { get; set; }
    }
}
