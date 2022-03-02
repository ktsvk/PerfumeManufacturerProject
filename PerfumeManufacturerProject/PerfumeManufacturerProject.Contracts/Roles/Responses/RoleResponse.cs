using System.Collections.Generic;

namespace PerfumeManufacturerProject.Contracts.Roles.Responses
{
    public sealed class RoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
