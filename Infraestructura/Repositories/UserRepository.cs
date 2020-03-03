using Domain;
using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Infraestructura.Repositories
{
    public class UserRepository : GenericRepository, IRespository<User>
    {
        public UserRepository(IDbContext context) : base(context) { }
        public User Add(User entity)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO dbo.users (username, password, role_id, firstname, lastname, state) OUTPUT INSERTED.* VALUES (@username, @password, @role_id, @firstname, @lastname, @state)");
                sqlCommand.Parameters.AddWithValue("@username", entity.Username);
                sqlCommand.Parameters.AddWithValue("@password", entity.Password);
                sqlCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
                sqlCommand.Parameters.AddWithValue("@firstname", entity.Firstname);
                sqlCommand.Parameters.AddWithValue("@lastname", entity.Lastname);
                sqlCommand.Parameters.AddWithValue("@state", entity.State);
                return new User(_context.Insert(sqlCommand));
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
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
            try
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM dbo.users WHERE id = @id");
                sqlCommand.Parameters.AddWithValue("@id", id);
                return _context.Delete(sqlCommand);
            }
            catch (Exception e)
            {
                return 0;
            }
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
           try
           {
                SqlCommand sqlCommand = new SqlCommand("UPDATE dbo.users SET username = @username, password = @password, role_id = @role_id, firstname = @firstname, lastname = @lastname, state = @state WHERE id = @id");
                sqlCommand.Parameters.AddWithValue("@username", entity.Username);
                sqlCommand.Parameters.AddWithValue("@password", entity.Password);
                sqlCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
                sqlCommand.Parameters.AddWithValue("@firstname", entity.Firstname);
                sqlCommand.Parameters.AddWithValue("@lastname", entity.Lastname);
                sqlCommand.Parameters.AddWithValue("@state", entity.State);
                sqlCommand.Parameters.AddWithValue("@id", entity.Id);
                return new User(_context.Update(sqlCommand));
           }
           catch (Exception e)
           {
               return null;
           }
        }

        public User Find(long id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM dbo.users WHERE id = @id");
                sqlCommand.Parameters.AddWithValue("@id", id);
                return new User(_context.Find(sqlCommand));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IList<User> GetAll(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                int start = (pageIndex - 1) * pageSize;
                IList<User> users = new List<User>();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM dbo.users OFFSET @inicio ROWS FETCH NEXT @size ROWS ONLY");
                sqlCommand.Parameters.AddWithValue("@inicio", start);
                sqlCommand.Parameters.AddWithValue("@size", pageSize);
                var data = _context.Select(sqlCommand);
                foreach (var row in data)
                {
                    users.Add(new User(row));
                }
                return users;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
