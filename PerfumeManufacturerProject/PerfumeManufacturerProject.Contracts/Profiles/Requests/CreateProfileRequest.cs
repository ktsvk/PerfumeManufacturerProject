using System.ComponentModel.DataAnnotations;

namespace PerfumeManufacturerProject.Contracts.Profiles.Requests
{
    public sealed class CreateProfileRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
