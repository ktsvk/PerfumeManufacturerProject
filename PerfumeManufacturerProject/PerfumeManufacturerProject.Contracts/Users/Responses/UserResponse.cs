using PerfumeManufacturerProject.Contracts.Profiles.Responses;

namespace PerfumeManufacturerProject.Contracts.Users.Responses
{
    public sealed class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public ProfileResponse Profile { get; set; }
    }
}
