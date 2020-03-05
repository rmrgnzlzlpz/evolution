using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Infraestructura.Repositories
{
    public class PermissionRepository : GenericRepository, IRepository<Permission>
    {
        private SqlCommand _dbCommand;
        public PermissionRepository(IDbContext context) : base(context) { }

        public Permission Add(Permission entity)
        {
            try
            {
                _dbCommand = new SqlCommand("INSERT INTO dbo.permissions (name, description, state) OUTPUT INSERTED.* VALUES (@name, @description, @state)");
                _dbCommand.Parameters.AddWithValue("@Name", entity.Name);
                _dbCommand.Parameters.AddWithValue("@Description", entity.Description);
                _dbCommand.Parameters.AddWithValue("@State", entity.State);
                return new Permission(_context.Insert(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int AddRange(IList<Permission> entities)
        {
            int count = 0;
            foreach (Permission permission in entities)
            {
                if (Add(permission) == null) break;
                count++;
            }
            return count;
        }

        public int Delete(long id)
        {
            try
            {
                _dbCommand = new SqlCommand("DELETE FROM dbo.permissions WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return _context.Delete(_dbCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int Delete(Permission entity)
        {
            return Delete(entity.Id);
        }

        public int DeleteRange(IList<Permission> entities)
        {
            int count = 0;
            foreach (Permission permission in entities)
            {
                if (Delete(permission.Id) < 1) break;
                count++;
            }
            return count;
        }

        public Permission Edit(Permission entity)
        {
            try
            {
                _dbCommand = new SqlCommand("UPDATE dbo.permissions SET name = @name, description = @description, state = @state WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@name", entity.Name);
                _dbCommand.Parameters.AddWithValue("@description", entity.Description);
                _dbCommand.Parameters.AddWithValue("@state", entity.State);
                return new Permission(_context.Update(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Permission Find(long id)
        {
            try
            {
                _dbCommand = new SqlCommand("SELECT * FROM dbo.permissions WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return new Permission(_context.Find(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<Permission> FindBy(string name, object value)
        {
            if (!name.Replace("_", "").All(char.IsLetterOrDigit)) return null;
            try
            {
                IList<Permission> entities = new List<Permission>();
                _dbCommand = new SqlCommand($"SELECT * FROM dbo.permissions where {name} = @value");
                _dbCommand.Parameters.AddWithValue("@value", value);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    entities.Add(new Permission(row));
                }
                return entities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<Permission> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                int start = (pageIndex - 1) * pageSize;
                IList<Permission> entities = new List<Permission>();
                _dbCommand = new SqlCommand("SELECT * FROM dbo.permissions OFFSET @inicio ROWS FETCH NEXT @size ROWS ONLY");
                _dbCommand.Parameters.AddWithValue("@inicio", start);
                _dbCommand.Parameters.AddWithValue("@size", pageSize);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    entities.Add(new Permission(row));
                }
                return entities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<Permission> GetByRole(long id)
        {
            try
            {
                IList<Permission> permissions = new List<Permission>();
                _dbCommand = new SqlCommand("SELECT permissions.* FROM permissions INNER JOIN roles_permissions ON permissions.id = roles_permissions.permission_id AND roles_permissions.role_id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    permissions.Add(new Permission(row));
                }
                return permissions;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
