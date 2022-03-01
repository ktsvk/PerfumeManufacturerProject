using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using PerfumeManufacturerProject.Contracts.Auth.Responses;
using PerfumeManufacturerProject.Contracts.Profiles.Responses;
using PerfumeManufacturerProject.Contracts.Users.Responses;
using PerfumeManufacturerProject.Contracts.Admin.Responses;

namespace PerfumeManufacturerProject.Business.Interfaces.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            DataModelsToServiceResultMaps();
            ServiceResultsToOutputContractMaps();
        }

        private void DataModelsToServiceResultMaps()
        {
            CreateMap<Profile, ProfileModel>();
            CreateMap<User, UserModel>();
        }

        private void ServiceResultsToOutputContractMaps()
        {
            CreateMap<LoginResult, LoginResponse>();

            CreateMap<ProfileModel, ProfileResponse>();
            CreateMap<UserModel, UserResponse>();
            CreateMap<UserModel, AdminResponse>();
        }
    }
}
