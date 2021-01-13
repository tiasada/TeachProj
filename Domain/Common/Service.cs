using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public class Service<T> : IService<T> where T : Entity
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual bool Remove(Guid id)
        {
            var entity = _repository.Get(id);
            
            if (entity == null)
            { return false; }
            
            _repository.Remove(entity);
            
            return true;
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