using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;

namespace PerfumeManufacturerProject.Business.Interfaces.Models.Auth
{
    public sealed class LoginResult
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public RoleModel Role { get; set; }
    }
}
