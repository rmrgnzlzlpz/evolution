using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositories
{
    public interface IRespository<T> where T : class
    {
        public T Add(T entity);
        public T Edit(T entity);
        public int Delete(long id);
        public T Find(long id);
        public int AddRange(IList<T> entities);
        public int DeleteRange(IList<T> entities);
        public IList<T> GetAll(int pageIndex = 0, int pageSize = 10);
    }
}
