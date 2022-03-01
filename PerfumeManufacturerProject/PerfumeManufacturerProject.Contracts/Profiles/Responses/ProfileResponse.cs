using System.Collections.Generic;

namespace PerfumeManufacturerProject.Contracts.Profiles.Responses
{
    public sealed class ProfileResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
