using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Infra.Generics
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        Guid? Remove(Guid id);

        T Get(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }
}