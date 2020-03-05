using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain.Entities
{
    public class Permission : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionState State { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public Permission(IDataRecord row) : base(row)
        {
            Name = row["name"].ToString();
            Description = row["description"].ToString();
            State = (PermissionState)Convert.ToInt32(row["state"]);
        }
    }
    public enum PermissionState
    {
        Inactive = 0,
        Active = 1,
    }
}
