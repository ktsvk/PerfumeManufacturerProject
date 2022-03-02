using PerfumeManufacturerProject.Contracts.Roles.Responses;

namespace PerfumeManufacturerProject.Contracts.Users.Responses
{
    public sealed class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public RoleResponse Role { get; set; }
    }
}
