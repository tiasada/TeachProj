using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class StudentsRepository : DatabaseRepository<Student>, IStudentsRepository
    {
        public override Student Get(Func<Student, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Students
                    .IgnoreAutoIncludes()
                    .Include(x => x.Parent)
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<Student> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Students
                    .IgnoreAutoIncludes()
                    .Include(x => x.Parent)
                    .ToList();
            }
        }

        public override IEnumerable<Student> GetAll(Func<Student, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}