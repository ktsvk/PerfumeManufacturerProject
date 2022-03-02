using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class RoleAlreadyExistsException : Exception
    {
        public RoleAlreadyExistsException(string name)
            : base($"Role with name: {name} already exists")
        {

        }
    }
}
