using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleState State { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }

    public enum RoleState
    {
        Inactive = 0,
        Active = 1
    }
}
