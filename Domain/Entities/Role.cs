using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleState State { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }

        public Role(IDataRecord row) : base(row)
        {
            Name = row["name"].ToString();
            State = (RoleState)Convert.ToInt32(row["state"]);
            Description = row["description"].ToString();
        }

        public Role()
        {

        }
    }

    public enum RoleState
    {
        Inactive = 0,
        Active = 1
    }
}
