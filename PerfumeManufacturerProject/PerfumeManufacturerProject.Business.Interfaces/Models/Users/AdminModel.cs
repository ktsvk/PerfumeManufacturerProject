using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;

namespace PerfumeManufacturerProject.Business.Interfaces.Models.Users
{
    public sealed class AdminModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
