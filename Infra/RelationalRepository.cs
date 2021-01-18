using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;

namespace Infra
{
    public class RelationalRepository<T> : IRelationalRepository<T> where T : class
    {
        public virtual void Add(T relation)
        {
            using (var db = new TeachContext())
            {
                db.Add<T>(relation);
                foreach (var m in db.Entry(relation).References)
                {
                    if (m.CurrentValue != null)
                    {
                        db.Entry(m.CurrentValue).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
        }

        public virtual void Remove(T relation)
        {
            using (var db = new TeachContext())
            {
                db.Set<T>().Remove(relation);
                db.SaveChanges();
            }
        }

        public virtual void Modify(T relation, T newRelation)
        {
            using (var db = new TeachContext())
            {
                db.Set<T>().Remove(relation);
                db.Add<T>(newRelation);
                
                db.Set<T>().Attach(newRelation);
                db.Entry(newRelation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
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
    }
}