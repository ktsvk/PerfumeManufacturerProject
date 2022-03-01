using System;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException()
            : base("Invalid login or password")
        {

        }
    }
}
