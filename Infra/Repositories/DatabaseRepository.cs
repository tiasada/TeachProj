using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Infra.Repositories
{
    public class DatabaseRepository<T> : IRepository<T> where T : class
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

        public virtual void Modify(T entity)
        {
            using (var db = new TeachContext())
            {
                db.Set<T>().Update(entity);
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

        public virtual IEnumerable<T> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Set<T>().Where(predicate).ToList();
            }
        }
    }
}