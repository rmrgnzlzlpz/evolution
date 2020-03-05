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
    public class RolePermissionRepository : GenericRepository, IRepository<RolePermission>
    {
        private string tableName = "roles_permissions";
        private SqlCommand _dbCommand;
        public RolePermissionRepository(IDbContext context) : base(context) { }
        public RolePermission Add(RolePermission entity)
        {
            try
            {
                _dbCommand = new SqlCommand($"INSERT INTO dbo.{tableName} (role_id, permission_id) OUTPUT INSERTED.* VALUES (@role_id, @permission_id)");
                _dbCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
                _dbCommand.Parameters.AddWithValue("@permission_id", entity.PermissionId);
                return new RolePermission(_context.Insert(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int AddRange(IList<RolePermission> entities)
        {
            int count = 0;
            foreach (RolePermission entity in entities)
            {
                if (Add(entity) == null) break;
                count++;
            }
            return count;
        }

        public int Delete(long id)
        {
            try
            {
                _dbCommand = new SqlCommand($"DELETE FROM dbo.{tableName} WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return _context.Delete(_dbCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public int Delete(RolePermission entity)
        {
            return Delete(entity.Id);
        }

        public int DeleteRange(IList<RolePermission> entities)
        {
            int count = 0;
            foreach (RolePermission entity in entities)
            {
                if (Delete(entity.Id) < 1) break;
                count++;
            }
            return count;
        }

        public RolePermission Edit(RolePermission entity)
        {
            try
            {
                _dbCommand = new SqlCommand($"UPDATE dbo.{tableName} SET role_id = @role_id, permission_id = @permission_id WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@role_id", entity.RoleId);
                _dbCommand.Parameters.AddWithValue("@permission_id", entity.PermissionId);
                return new RolePermission(_context.Update(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public RolePermission Find(long id)
        {
            try
            {
               _dbCommand = new SqlCommand($"SELECT * FROM dbo.{tableName} WHERE id = @id");
                _dbCommand.Parameters.AddWithValue("@id", id);
                return new RolePermission(_context.Find(_dbCommand));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<RolePermission> FindBy(string name, object value)
        {
            if (!name.Replace("_", "").All(char.IsLetterOrDigit)) return null;
            IList<RolePermission> entities = new List<RolePermission>();
            try
            {
                _dbCommand = new SqlCommand($"SELECT * FROM dbo.{tableName} WHERE {name} = @value");
                _dbCommand.Parameters.AddWithValue("@value", value);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    entities.Add(new RolePermission(row));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return entities;
        }

        public IList<RolePermission> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                int start = (pageIndex - 1) * pageSize;
                IList<RolePermission> entities = new List<RolePermission>();
                _dbCommand = new SqlCommand($"SELECT * FROM dbo.{tableName} OFFSET @inicio ROWS FETCH NEXT @size ROWS ONLY");
                _dbCommand.Parameters.AddWithValue("@inicio", start);
                _dbCommand.Parameters.AddWithValue("@size", pageSize);
                var data = _context.Select(_dbCommand);
                foreach (var row in data)
                {
                    entities.Add(new RolePermission(row));
                }
                return entities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
