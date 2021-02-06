using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Remove(T entity);

        void Modify(T entity);

        T Get(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> predicate);
    }
}