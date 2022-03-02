using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Admin.Requests
{
    public sealed class CreateAdminRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
