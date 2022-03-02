using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string name)
            : base($"Role with name: {name} not found")
        {

        }
    }
}
