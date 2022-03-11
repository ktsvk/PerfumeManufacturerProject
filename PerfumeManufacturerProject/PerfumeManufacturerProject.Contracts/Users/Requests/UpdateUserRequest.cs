using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Users.Requests
{
    public sealed class UpdateUserRequest
    {
        [Required]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
