using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionState State { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }

    public enum PermissionState
    {
        Inactive = 0,
        Active = 1,
    }
}
