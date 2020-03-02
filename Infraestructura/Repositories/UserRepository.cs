using Domain;
using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infraestructura.Repositories
{
    public class UserRepository : GenericRepository, IRespository<User>
    {
        public UserRepository(IDbContext context) : base(context) { }
        public User Add(User entity)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO dbo.users (username, password, role_id, firstname, lastname, state) VALUES (@username, @password, @role_id, @firstname, @lastname, @state)");
            sqlCommand.Parameters.AddWithValue("@username", entity.Username);
            sqlCommand.Parameters.AddWithValue("@password", entity.Password);
            sqlCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
            sqlCommand.Parameters.AddWithValue("@firstname", entity.Firstname);
            sqlCommand.Parameters.AddWithValue("@lastname", entity.Lastname);
            sqlCommand.Parameters.AddWithValue("@state", entity.State);
            if (_context.ModifierQuery(sqlCommand) > 0)
            {
                return entity;
            }
            return null;
        }

        public int AddRange(IList<User> entities)
        {
            int count = 0;
            foreach (User user in entities)
            {
                if (Add(user) == null) break;
                count++;
            }
            return count;
        }

        public int Delete(long id)
        {
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM dbo.users WHERE id = @id");
            sqlCommand.Parameters.AddWithValue("@id", id);
            return _context.ModifierQuery(sqlCommand);
        }

        public int DeleteRange(IList<User> entities)
        {
            int count = 0;
            foreach (User user in entities)
            {
                if (Delete(user.Id) < 1) break;
                count++;
            }
            return count;
        }

        public User Edit(User entity)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE dbo.users SET username = @username, password = @password, role_id = @role_id, firstname = @firstname, lastname = @lastname, state = @state WHERE id = @id");
            sqlCommand.Parameters.AddWithValue("@username", entity.Username);
            sqlCommand.Parameters.AddWithValue("@password", entity.Password);
            sqlCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
            sqlCommand.Parameters.AddWithValue("@firstname", entity.Firstname);
            sqlCommand.Parameters.AddWithValue("@lastname", entity.Lastname);
            sqlCommand.Parameters.AddWithValue("@state", entity.State);
            sqlCommand.Parameters.AddWithValue("@id", entity.Id);
            if (_context.ModifierQuery(sqlCommand) > 0)
            {
                return entity;
            }
            return null;
        }

        public User Find(long id)
        {
            User entity = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM dbo.users WHERE id = @id");
            sqlCommand.Parameters.AddWithValue("@id", id);
            var data = _context.Select(sqlCommand);
            foreach (var row in data)
            {
                entity = new User
                {
                    Id = long.Parse(row["id"].ToString()),
                    Firstname = row["firstname"].ToString(),
                    Lastname = row["lastname"].ToString(),
                    Password = row["password"].ToString(),
                    RoleId = long.Parse(row["role_id"].ToString()),
                    State = (UserState)Convert.ToInt32(row["state"]),
                    Username = row["username"].ToString()
                };
            }
            return entity;
        }

        public IList<User> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
