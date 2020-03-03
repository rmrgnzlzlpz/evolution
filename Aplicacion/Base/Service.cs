using Aplicacion.Interface;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Base
{
    public abstract class Service<T> : IService<T> where T : class
    {
        protected readonly IRespository<T> _repository;
        public Service(IRespository<T> repository)
        {
            _repository = repository;
        }
        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new Exception($"Entity {entity.GetType().Name} is null");
            }
            try
            {
                return _repository.Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception($"Entity error: {e.Message}");
            }
        }

        public virtual bool Delete(T entity)
        {
            if (entity == null) throw new Exception($"Entity {entity.GetType().Name} is null");
            try
            {
                return _repository.Delete(entity) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw new Exception($"Entity error: {e.Message}");
            }
        }

        public virtual bool Delete(long id)
        {
            if (id < 1) throw new Exception("id is null");
            try
            {
                return _repository.Delete(id) > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw new Exception($"Entity error: {e.Message}");
            }
        }

        public virtual T Find(long id)
        {
            if (id < 1) throw new Exception("id is null");
            try
            {
                return _repository.Find(id);
            }
            catch (Exception e)
            {
                throw new Exception($"Entity error: {e.Message}");
            }
        }

        public virtual IEnumerable<T> GetAll(int index = 0, int size = 10)
        {
            if (index < 0 || size < 0) throw new Exception("Paginate with negative number");
            try
            {
                return _repository.GetAll(index, size);
            }
            catch (Exception e)
            {
                throw new Exception($"Entity error: {e.Message}");
            }
        }

        public virtual T Update(T entity)
        {
            if (entity == null) throw new Exception($"Entity {entity.GetType().Name} is null");
            try
            {
                return _repository.Edit(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Entity " + e.Message.ToString());
            }
        }
    }
}
