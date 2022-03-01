using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Auth.Requests
{
    public sealed class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
