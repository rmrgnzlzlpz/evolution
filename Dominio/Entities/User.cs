using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public UserState State { get; set; }
        public long RoleId { get; set; }
        public Role Role {get; set; }
    }

    public enum UserState
    {
        Inactive = 0,
        Active = 1,
    }
}
