using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Teachers;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TeachersRepository : DatabaseRepository<Teacher>, ITeachersRepository
    {
        public override Teacher Get(Func<Teacher, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Teachers
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<Teacher> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Teachers
                    .ToList();
            }
        }

        public override IEnumerable<Teacher> GetAll(Func<Teacher, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}