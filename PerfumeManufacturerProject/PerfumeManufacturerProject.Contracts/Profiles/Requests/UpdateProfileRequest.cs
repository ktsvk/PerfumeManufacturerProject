using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Profiles.Requests
{
    public sealed class UpdateProfileRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
