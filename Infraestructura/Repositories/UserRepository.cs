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
    public class UserRepository : GenericRepository, IRepository<User>
    {
        private SqlCommand _dbCommand;
        public UserRepository(IDbContext context) : base(context) { }
        public User Add(User entity)
        {
            try
            {
                _dbCommand = new SqlCommand("INSERT INTO dbo.users (username, password, role_id, firstname, lastname, state) OUTPUT INSERTED.* VALUES (@username, @password, @role_id, @firstname, @lastname, @state)");
                _dbCommand.Parameters.AddWithValue("@username", entity.Username);
                _dbCommand.Parameters.AddWithValue("@password", entity.Password);
                _dbCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
                _dbCommand.Parameters.AddWithValue("@firstname", entity.Firstname);
                _dbCommand.Parameters.AddWithValue("@lastname", entity.Lastname);
                _dbCommand.Parameters.AddWithValue("@state", entity.State);
                return new User(_context.Insert(_dbCommand));
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _context.Close();
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
                _dbCommand = new SqlCommand("DELETE FROM dbo.users WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return _context.Delete(_dbCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int Delete(User entity)
        {
            return Delete(entity.Id);
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
                _dbCommand = new SqlCommand("UPDATE dbo.users SET username = @username, password = @password, role_id = @role_id, firstname = @firstname, lastname = @lastname, state = @state WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@username", entity.Username);
                _dbCommand.Parameters.AddWithValue("@password", entity.Password);
                _dbCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
                _dbCommand.Parameters.AddWithValue("@firstname", entity.Firstname);
                _dbCommand.Parameters.AddWithValue("@lastname", entity.Lastname);
                _dbCommand.Parameters.AddWithValue("@state", entity.State);
                _dbCommand.Parameters.AddWithValue("@id", entity.Id);
                return new User(_context.Update(_dbCommand));
           }
           catch (Exception e)
           {
                Console.WriteLine(e.Message);
                return null;
           }
        }

        public User Find(long id)
        {
            try
            {
                _dbCommand = new SqlCommand("SELECT * FROM dbo.users WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return new User(_context.Find(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<User> FindBy(string name, object value)
        {
            if (!name.Replace("_", "").All(char.IsLetterOrDigit)) return null;
            IList<User> users = new List<User>();
            try
            {
                _dbCommand = new SqlCommand($"SELECT * FROM dbo.users WHERE {name} = @value");
                _dbCommand.Parameters.AddWithValue("@value", value);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    users.Add(new User(row));
                }
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<User> GetAll(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                int start = (pageIndex - 1) * pageSize;
                IList<User> users = new List<User>();
                _dbCommand = new SqlCommand("SELECT * FROM dbo.users OFFSET @inicio ROWS FETCH NEXT @size ROWS ONLY");
                _dbCommand.Parameters.AddWithValue("@inicio", start);
                _dbCommand.Parameters.AddWithValue("@size", pageSize);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    users.Add(new User(row));
                }
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
