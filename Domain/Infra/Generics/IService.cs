using System;
using System.Collections.Generic;

namespace Domain.Infra.Generics
{
    public interface IService<T> where T : Entity
    {
        Guid? Remove(Guid id);

        T Get(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }
}