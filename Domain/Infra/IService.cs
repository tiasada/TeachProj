using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Infra
{
    public interface IService<T> where T : Entity
    {
        Guid? Remove(Guid id);

        T Get(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }
}