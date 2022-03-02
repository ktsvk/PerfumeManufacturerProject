using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class PermissionNotFoundException : Exception
    {
        public PermissionNotFoundException(string id)
            : base($"Permission with id: {id} not found")
        {

        }
    }
}
