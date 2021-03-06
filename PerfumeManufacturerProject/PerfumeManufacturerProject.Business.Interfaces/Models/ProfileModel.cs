using System.Collections.Generic;

namespace PerfumeManufacturerProject.Business.Interfaces.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
