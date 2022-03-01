using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class ProfileAlreadyExistsException : Exception
    {
        public ProfileAlreadyExistsException(string name)
            : base($"Profile with name: {name} already exists")
        {

        }
    }
}
