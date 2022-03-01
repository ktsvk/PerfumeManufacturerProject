using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfumeManufacturerProject.Business.Interfaces.Exceptions.Auth
{
    public class ErrorDuringRegisterException : Exception
    {
        public ErrorDuringRegisterException(IEnumerable<IdentityError> errors)
            : base($"{string.Join(";", errors.Select(x => x.Description))}")
        {
            
        }
    }
}
