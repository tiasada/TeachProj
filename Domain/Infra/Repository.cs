using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Infra
{
    class Repository<T> where T : Entity
    {
        public void Add(T entity)
        {
            using (var db = new TeachContext())
            {
                db.Add<T>(entity);
                db.SaveChanges();
            }
        }

        public Guid? Remove(Guid id)
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

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().FirstOrDefault(predicate);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().ToList();
            }
        }
    }
}