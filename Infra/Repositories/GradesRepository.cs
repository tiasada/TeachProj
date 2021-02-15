using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Grades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class GradesRepository : DatabaseRepository<Grade>, IGradesRepository
    {
        public override Grade Get(Func<Grade, bool> predicate)
        {
            using (var db = new TeachContext())
            {
                return db.Grades
                    .Include(x => x.Classroom)
                    .Include(x => x.StudentGrades).ThenInclude(s => s.Student)
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<Grade> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Grades
                    .Include(x => x.Classroom)
                    .Include(x => x.StudentGrades).ThenInclude(s => s.Student)
                    .ToList();
            }
        }

        public override IEnumerable<Grade> GetAll(Func<Grade, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}