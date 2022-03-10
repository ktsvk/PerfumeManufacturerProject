using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions.Roles
{
    public class CannotDeleteRoleException : Exception
    {
        public CannotDeleteRoleException(string roleName)
            : base($"Cannot delete role: {roleName} because there are already users with this role")
        {

        }
    }
}
