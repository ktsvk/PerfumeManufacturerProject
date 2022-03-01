namespace PerfumeManufacturerProject.Business.Interfaces.Models
{
    public sealed class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public ProfileModel Profile { get; set; }
    }
}
