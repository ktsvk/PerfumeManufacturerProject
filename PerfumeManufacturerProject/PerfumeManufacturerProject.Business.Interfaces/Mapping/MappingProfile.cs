using AutoMapper;
using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using PerfumeManufacturerProject.Contracts.Auth.Responses;
using PerfumeManufacturerProject.Contracts.Roles.Responses;
using PerfumeManufacturerProject.Contracts.Users.Responses;
using PerfumeManufacturerProject.Contracts.Admin.Responses;

namespace PerfumeManufacturerProject.Business.Interfaces.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            DataModelsToServiceResultMaps();
            ServiceResultsToOutputContractMaps();
        }

        private void DataModelsToServiceResultMaps()
        {
            CreateMap<ApplicationRole, RoleModel>();
            CreateMap<ApplicationUser, UserModel>();
        }

        private void ServiceResultsToOutputContractMaps()
        {
            CreateMap<LoginResult, LoginResponse>();

            CreateMap<RoleModel, RoleResponse>();
            CreateMap<UserModel, UserResponse>();
            CreateMap<UserModel, AdminResponse>();
        }
    }
}
