using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Admin.Requests
{
    public sealed class UpdateAdminRequest
    {
        [Required]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
