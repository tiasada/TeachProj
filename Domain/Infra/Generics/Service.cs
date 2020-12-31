using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Infra.Generics
{
    public class Service<T> : IService<T> where T : Entity
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual Guid? Remove(Guid id)
        {
            return _repository.Remove(id);
        }

        public virtual T Get(Func<T, bool> predicate)
        {
            return _repository.Get(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}