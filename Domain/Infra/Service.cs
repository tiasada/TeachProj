using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Infra
{
    public class Service<T> : IService<T> where T : Entity
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Guid? Remove(Guid id)
        {
            return _repository.Remove(id);
        }

        public T Get(Func<T, bool> predicate)
        {
            return _repository.Get(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}