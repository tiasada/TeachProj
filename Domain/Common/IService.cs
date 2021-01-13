using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public interface IService<T> where T : Entity
    {
        bool Remove(Guid id);

        T Get(Func<T, bool> predicate);
        T Get(Guid id);

        IEnumerable<T> GetAll();
    }
}