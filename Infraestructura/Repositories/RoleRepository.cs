using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Infraestructura.Repositories
{
    public class RoleRepository : GenericRepository, IRespository<Role>
    {
        private SqlCommand _dbCommand;
        public RoleRepository(IDbContext context) : base(context) { }
        public Role Add(Role entity)
        {
            try
            {
                _dbCommand = new SqlCommand("INSERT INTO dbo.roles (name, description, state) OUTPUT INSERTED.* VALUES (@name, @description, @state)");
                _dbCommand.Parameters.AddWithValue("@Name", entity.Name);
                _dbCommand.Parameters.AddWithValue("@Description", entity.Description);
                _dbCommand.Parameters.AddWithValue("@State", entity.State);
                return new Role(_context.Insert(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int AddRange(IList<Role> entities)
        {
            int count = 0;
            foreach (Role role in entities)
            {
                if (Add(role) == null) break;
                count++;
            }
            return count;
        }

        public int Delete(long id)
        {
            try
            {
                _dbCommand = new SqlCommand("DELETE FROM dbo.roles WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return _context.Delete(_dbCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int Delete(Role entity)
        {
            return Delete(entity.Id);
        }

        public int DeleteRange(IList<Role> entities)
        {
            int count = 0;
            foreach (Role role in entities)
            {
                if (Delete(role.Id) < 1) break;
                count++;
            }
            return count;
        }

        public Role Edit(Role entity)
        {
            try
            {
                _dbCommand = new SqlCommand("UPDATE dbo.roles SET name = @name, description = @description, state = @state WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@name", entity.Name);
                _dbCommand.Parameters.AddWithValue("@description", entity.Description);
                _dbCommand.Parameters.AddWithValue("@state", entity.State);
                return new Role(_context.Update(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Role Find(long id)
        {
            try
            {
               _dbCommand = new SqlCommand("SELECT * FROM dbo.roles WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return new Role(_context.Find(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<Role> FindBy(string name, object value)
        {
            if (!name.Replace("_", "").All(char.IsLetterOrDigit)) return null;
            IList<Role> roles = new List<Role>();
            try
            {
                _dbCommand = new SqlCommand($"SELECT * FROM dbo.roles WHERE {name} = @value");
                _dbCommand.Parameters.AddWithValue("@value", value);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    roles.Add(new Role(row));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return roles;
        }

        public IList<Role> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                int start = (pageIndex - 1) * pageSize;
                IList<Role> roles = new List<Role>();
                _dbCommand = new SqlCommand("SELECT * FROM dbo.roles OFFSET @inicio ROWS FETCH NEXT @size ROWS ONLY");
                _dbCommand.Parameters.AddWithValue("@inicio", start);
                _dbCommand.Parameters.AddWithValue("@size", pageSize);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    roles.Add(new Role(row));
                }
                return roles;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
