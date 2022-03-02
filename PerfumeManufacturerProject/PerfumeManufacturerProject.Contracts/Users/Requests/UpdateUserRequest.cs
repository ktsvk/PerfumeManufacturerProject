using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Users.Requests
{
    public sealed class UpdateUserRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
