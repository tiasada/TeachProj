using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Remove(T entity);

        void Modify(Guid id, T newEntity);

        T Get(Func<T, bool> predicate);
        T Get(Guid id);

        IEnumerable<T> GetAll();
    }
}