using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IRepository<T> _repository;

        public Repository(IRepository<T> repository)
        {
            _repository = repository;
        }
        
        public virtual void Add(T entity)
        {
            _repository.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            _repository.Remove(entity);
        }

        public virtual void Modify(Guid id, T newEntity)
        {
            _repository.Modify(id, newEntity);
        }

        public virtual T Get(Func<T, bool> predicate)
        {
            return _repository.Get(predicate);
        }

        public virtual T Get(Guid id)
        {
            return _repository.Get(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}