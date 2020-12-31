using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Infra.Generics
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public virtual void Add(T entity)
        {
            using (var db = new TeachContext())
            {
                db.Add<T>(entity);
                db.SaveChanges();
            }
        }

        public virtual Guid? Remove(Guid id)
        {
            using (var db = new TeachContext())
            {
                var classroom = db.Classrooms.FirstOrDefault(x => x.Id == id);
                if (classroom == null) {return null;}
                db.Classrooms.Remove(classroom);
                db.SaveChanges();
                return id;
            }
        }

        public virtual T Get(Func<T, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().FirstOrDefault(predicate);
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().ToList();
            }
        }
    }
}