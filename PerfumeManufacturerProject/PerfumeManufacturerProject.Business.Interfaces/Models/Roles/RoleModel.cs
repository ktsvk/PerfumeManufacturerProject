﻿using System.Collections.Generic;

namespace PerfumeManufacturerProject.Business.Interfaces.Models.Roles
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PermissionModel> Permissions { get; set; }
    }
}
