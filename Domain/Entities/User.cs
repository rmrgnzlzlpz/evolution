using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public UserState State { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public User(IDataRecord row) : base(row)
        {
            Username = row["username"].ToString();
            Password = row["password"].ToString();
            State = (UserState)Convert.ToInt32(row["state"]);
            Firstname = row["firstname"].ToString();
            Lastname = row["lastname"].ToString();
            RoleId = long.Parse(row["role_id"].ToString());
        }

        public User()
        {

        }
    }

    public enum UserState
    {
        Inactive = 0,
        Active = 1,
    }
}
