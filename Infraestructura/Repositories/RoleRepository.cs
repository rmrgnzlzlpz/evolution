using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infraestructura.Repositories
{
    public class RoleRepository : GenericRepository, IRespository<Role>
    {
        public RoleRepository(IDbContext context) : base(context) { }
        public Role Add(Role entity)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO dbo.roles (name, description, state) VALUES (@Name, @Description, @State)");
            sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
            sqlCommand.Parameters.AddWithValue("@Description", entity.Description);
            sqlCommand.Parameters.AddWithValue("@State", entity.State);
            int count = _context.ModifierQuery(sqlCommand);

            return count > 0 ? entity : null;
        }

        public void AddRange(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IList<Role> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IList<Role> entities)
        {
            throw new NotImplementedException();
        }

        public Role Edit(Role entity)
        {
            throw new NotImplementedException();
        }

        public Role Find(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        int IRespository<Role>.Delete(long id)
        {
            throw new NotImplementedException();
        }

        IList<Role> IRespository<Role>.GetAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
