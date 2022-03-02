using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string id)
            : base($"Role with id: {id} not found")
        {

        }
    }
}
