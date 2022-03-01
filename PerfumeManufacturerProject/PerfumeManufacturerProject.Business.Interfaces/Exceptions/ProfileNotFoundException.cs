using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class ProfileNotFoundException : Exception
    {
        public ProfileNotFoundException(string name)
            : base($"Profile with name: {name} not found")
        {

        }
    }
}
