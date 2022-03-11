using AutoMapper;
using PerfumeManufacturerProject.Business.Interfaces.Models.Auth;
using PerfumeManufacturerProject.Business.Interfaces.Models.Roles;
using PerfumeManufacturerProject.Business.Interfaces.Models.Users;
using PerfumeManufacturerProject.Data.Interfaces.Models;
using PerfumeManufacturerProject.Contracts.Auth.Responses;
using PerfumeManufacturerProject.Contracts.Roles.Responses;
using PerfumeManufacturerProject.Contracts.Users.Responses;
using PerfumeManufacturerProject.Contracts.Admin.Responses;
using PerfumeManufacturerProject.Data.Interfaces.Models.Auth;

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
            CreateMap<ApplicationUser, AdminModel>();
            CreateMap<User, UserModel>();
            CreateMap<Role, RoleModel>();
            CreateMap<Permission, PermissionModel>();
        }

        private void ServiceResultsToOutputContractMaps()
        {
            CreateMap<LoginResult, LoginResponse>();

            CreateMap<AdminModel, AdminResponse>();
            CreateMap<UserModel, UserResponse>();
            CreateMap<RoleModel, RoleResponse>();
            CreateMap<PermissionModel, PermissionResponse>();
        }
    }
}
