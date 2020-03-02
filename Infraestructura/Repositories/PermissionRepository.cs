using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositories
{
    public class PermissionRepository : GenericRepository, IRespository<Permission>
    {
        public PermissionRepository(IDbContext context) : base(context) { }
        public Permission Add(Permission entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Permission> entities)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IList<Permission> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Permission> entities)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IList<Permission> entities)
        {
            throw new NotImplementedException();
        }

        public Permission Edit(Permission entity)
        {
            throw new NotImplementedException();
        }

        public Permission Find(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Permission> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        int IRespository<Permission>.Delete(long id)
        {
            throw new NotImplementedException();
        }

        IList<Permission> IRespository<Permission>.GetAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
