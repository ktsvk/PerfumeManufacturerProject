using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Users.Requests
{
    public sealed class CreateUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
