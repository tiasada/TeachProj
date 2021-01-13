using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IRelationalRepository<T> where T : class
    {
        void Add(T relation);

        void Remove(T relation);

        void Modify(T relation, T newRelation);

        T Get(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }
}