using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interface
{
    public interface IService<T> where T : class
    {
        public T Find(long id);
        public T Create(T entity);
        public bool Delete(T entity);
        public bool Delete(long id);
        public IEnumerable<T> GetAll(int index = 0, int size = 10);
        public T Update(T entity);
    }
}
