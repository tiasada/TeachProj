using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Infra
{
    public class DatabaseRepository<T> : IRepository<T> where T : Entity
    {
        public virtual void Add(T entity)
        {
            using (var db = new TeachContext())
            {
                db.Add<T>(entity);
                foreach (var m in db.Entry(entity).References)
                {
                    if (m.CurrentValue != null)
                    {
                        db.Entry(m.CurrentValue).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
        }

        public virtual void Remove(T entity)
        {
            using (var db = new TeachContext())
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        public virtual void Modify(Guid id, T newEntity)
        {
            using (var db = new TeachContext())
            {
                var oldEntity = db.Set<T>().FirstOrDefault(x => x.Id == id);
                newEntity.Id = oldEntity.Id;
                
                db.Set<T>().Remove(oldEntity);
                db.Add<T>(newEntity);
                
                db.Set<T>().Attach(newEntity);
                db.Entry(newEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
                db.SaveChanges();
            }
        }

        public virtual T Get(Func<T, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().FirstOrDefault(predicate);
            }
        }

        public virtual T Get(Guid id)
        {
            return this.Get(x => x.Id == id);
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