using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Parents;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ParentsRepository : DatabaseRepository<Parent>, IParentsRepository
    {
        public override Parent Get(Func<Parent, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Parents
                    .Include(x => x.Student)
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<Parent> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Parents
                    .Include(x => x.Student)
                    .ToList();
            }
        }

        public override IEnumerable<Parent> GetAll(Func<Parent, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}